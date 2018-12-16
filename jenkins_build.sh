#!/bin/sh

#### Set On Jenkins ###

export DEVELOPER_DIR="/Applications/Xcode.app/Contents/Developer"
WORKSPACE=`pwd`

SCHEME=project
CONFIGURATION=Debug # Debug, Release and so on

PROVISIONING_NAME=16d85a31-6ca2-49ca-8226-be76652ab9e7
IPA_FILE_NAME=develop-adhoc.ipa
iOS_SDK=iphoneos

# Get the ProvisioningProfile UUID
echo "************** Get the ProvisioningProfile UUID ***************"
KEYCHAIN=${HOME}/Library/Keychains/login.keychain
security cms -D -k ${KEYCHAIN} -i ${PROVISIONING_NAME}.mobileprovision > mobileprovision
plutil -extract 'UUID' xml1 mobileprovision
MPNAME=`/usr/libexec/PlistBuddy -c "Print $UUID" mobileprovision`
rm mobileprovision

# Install ProvisioningProfile
cp ${PROVISIONING_NAME}.mobileprovision ${HOME}/Library/MobileDevice/Provisioning\ Profiles/${MPNAME}.mobileprovision
PROVISIONING_PATH=\"${HOME}/Library/MobileDevice/Provisioning\ Profiles/${MPNAME}.mobileprovision\"
echo $PROVISIONING_PATH

# Unlock keychain
echo "************** Unlock keychain ****************"
/usr/bin/security default-keychain -d user -s ${KEYCHAIN}
/usr/bin/security unlock-keychain -p user ${KEYCHAIN}
/usr/bin/security find-identity -p codesigning -v

# Build project
echo "************** Build project ****************"
BUILD_DIR=${WORKSPACE}/build
/usr/bin/xcodebuild -scheme ${SCHEME} -workspace ${WORKSPACE}/project/Unity-iPhone.xcodeproj -sdk ${iOS_SDK} -configuration ${CONFIGURATION} clean build CODE_SIGN_IDENTITY="iPhone Developer" PROVISIONING_PROFILE=${MPNAME} CONFIGURATION_BUILD_DIR=${BUILD_DIR}

# Create ipa
echo "************** Create ipa file ****************"
IPA_FILE=${BUILD_DIR}/${IPA_FILE_NAME}
/usr/bin/xcrun -sdk ${iOS_SDK} PackageApplication -v ${BUILD_DIR}/${SCHEME}.app -o ${IPA_FILE} --embed ${PROVISIONING_PATH}

# Upload to DeployGate
echo "************** Upload ipa to DeployGate ****************"
NOTE="hoge"
MESSAGE=`git log --pretty=format:%x0a%s%x0a%b -1`

curl -F "token=${DG_API_KEY}" \
-F "message=${GIT_BRANCH}-${MESSAGE}" \
-F "distribution_key=${DG_DISTRIBUTION_KEY}" \
-F "release_note=${NOTE}" \
-F "file=@${IPA_FILE}" \
https://deploygate.com/api/users/${DG_USER}/apps
#!/bin/bash
 
echo "Uploading IPA to Appstore Connect..."
 
# set up in cloud build environment variables 
#ITUNES_USERNAME="firegate666@googlemail.com"
#ITUNES_PASSWORD="<put your app generated password here>" 
 
#Path is "/BUILD_PATH/<ORG_ID>.<PROJECT_ID>.<BUILD_TARGET_ID>/.build/last/<BUILD_TARGET_ID>/build.ipa"
path="$WORKSPACE/.build/last/ios-store/build.ipa"
 
if xcrun altool --upload-app -f $path -u $ITUNES_USERNAME -p $ITUNES_PASSWORD ; then
    echo "Upload IPA to Appstore Connect finished with success"
else
    echo "Upload IPA to Appstore Connect failed"
fi


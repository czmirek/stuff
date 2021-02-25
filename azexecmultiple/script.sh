#!/bin/bash

# This script allows you to automate running multiple commands over az container exec by using a screen multiplexer
# It installs AzureCLI inside the multiplexer terminal and logins with provided environmental variables which are
# provided with "addSpnToEnvironment:true" in the task AzureCLI@2:
#    $servicePrincipalId
#    $servicePrincipalKey
#    $tenantId
#
# See https://stackoverflow.com/questions/66356445/automate-az-container-exec for more info

echo "Installing screen..."
apt-get -qq install screen

echo "Creating multiplex terminal"
screen -S azexec -dmL bash
> screenlog.0

echo "Installing AZ CLI in multiplex terminal"
screen -S azexec -p 0 -X stuff "curl -sL https://aka.ms/InstallAzureCLIDeb | sudo bash && echo \"ENDINSTALLAZCLI\"^M" &
tail -f screenlog.0 | sed '/^ENDINSTALLAZCLI[[:cntrl:]]*$/ q'
> screenlog.0

echo "AZ LOGIN inside multiplex terminal"
screen -S azexec -p 0 -X stuff "az login --service-principal --username ${servicePrincipalId} --password \"${servicePrincipalKey}\" --tenant ${tenantId} >/dev/null && echo \"\" && echo \"ENDAZLOGIN\"^M" &
tail -f screenlog.0 | sed '/^ENDAZLOGIN[[:cntrl:]]*$/ q'
> screenlog.0

screen -S azexec -p 0 -X stuff "az container exec -g myresourcegroup -n nginxtest --exec-command /bin/bash^M" &
until grep "^root@SandboxHost-.*" screenlog.0 > /dev/null;
do
    echo "Waiting for container terminal..."
    sleep 5
done

echo "Terminal connected!"

# Run whatever you want on the container like this. Don't forget to add ^M at the end of your scripts to simulate enter key in the terminal
screen -S azexec -p 0 -X stuff "echo \"I'm running automated commands over ACI\"^M" &



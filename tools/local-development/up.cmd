docker-compose up -d

timeout 15 # sleep 10 seconds to give time to docker to finish the setup
echo setup vault configuration
./tools/vault/config.cmd
echo completed
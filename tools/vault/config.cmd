REM this is the code for the first part of the tutorial. 
REM example code for get KeyValue secrets. 
docker exec -it vault vault kv put secret/examplesecret secretUser=userfromVault secrectPassword=PasswordFromVault

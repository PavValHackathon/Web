echo "waiting for the service to start"

ATTEMPT=1
ATTEMPT_LIMIT=5

while [ "$(curl -sL -o /dev/null -w ''%{http_code}'' localhost:4447/health)" != "200" ]
do
  if [ "$ATTEMPT" -gt "$ATTEMPT_LIMIT" ]; then
    echo "service is unhealthy, number of attempts=$ATTEMPT is exceeded"
    exit 1
  fi
  
  echo "attempt=$ATTEMPT, sleeping for 5 seconds"
  sleep 5
  ATTEMPT=$[$ATTEMPT + 1]
done

echo "service is healthy, attempts=$ATTEMPT"
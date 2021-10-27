# Build and run

1. Start everything and attach to CLI:

```
docker-compose run cli
```

2. Paste App Insights instrumentation key into CLI.
3. Play with scenarios 1-2-3 in any order.
4. Quit CLI with `q` to force telemetry flushing to the cloud.
5. Wait a minute.
6. Press `Ctrl`+`C` to stop `cli` container.
7. Stop everything else:

```
docker-compose down
```

# Answer questions

1. Why don’t we see a client (CLI) there?
2. What is rate of failures for a client?
3. What is top 3 failing requests for a client?
4. What of the failing client requests are a problem (while others are not)? Why others are not?
5. What is the root cause of the problem (what exception? in which service? at which line?)?
6. Which path is the hottest one (has most of the requests)?
7. Which service receives most of the requests?
8. What is the slowest request for a client in average?
9. What is the client request time in 95th percentile?
10. Where the slowest client requests spend most of the time?
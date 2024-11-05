#!/bin/bash
# use ANSI codes for colored terminal output.
RED='\033[31m'
GREEN='\033[32m'
RESET='\033[0m'

if [ "$#" -ne 1 ]; then # exit if number of arguments != 1
    echo -e "${RED}ERROR: Bad number of arguments${RESET}"
    echo "Usage: <.sql file>"
    exit 1
fi

if [[ ! "$1" == *.sql ]]; then # exit arg 1 does not end with .sql
    echo -e "${RED}ERROR: Bad file arguement${RESET}"
    echo "File must be of type '.sql'"
    exit 1
fi

if [[ ! -f $1 ]]; then # exit if .sql file not found
    echo -e "${RED}ERROR: .sql file not found: '$1'${RESET}"
    exit 1
fi

# store the passed argument
SQL_FILE=$1

# store the content of the .sql file
QUERY=$(cat "$SQL_FILE")

# run the query against the database using sqlcmd.
# hardcoded credentials are not a great idea, but this is just to get the job done during the dev phase.
sqlcmd -S 37.27.179.21\\SQLEXPRESS22,1433 -U 'sa' -P 'itsteatime-123' -d ERP -Q "$QUERY"

if [ $? -eq 0 ]; then # exit code - 0 is success.
    echo -e "${GREEN}Database query successfully ran.${RESET}"
else
    echo -e "${RED}Database query failed.${RESET}"
fi


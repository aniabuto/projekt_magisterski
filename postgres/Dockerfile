FROM postgres:14.3

#COPY init-user-db.sh /docker-entrypoint-initdb.d/init-user-p.sh
COPY postgres_create.sql /docker-entrypoint-initdb.d/postgres_create.sql

ENV POSTGRES_USER safe
ENV POSTGRES_PASSWORD 1234
ENV POSTGRES_DB sample
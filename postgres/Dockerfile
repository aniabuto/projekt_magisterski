FROM postgres
COPY postgres_create.sql /docker-entrypoint-initdb.d/postgres_create.sql

ENV POSTGRES_USER safe
ENV POSTGRES_PASSWORD 1234
ENV POSTGRES_DB sample
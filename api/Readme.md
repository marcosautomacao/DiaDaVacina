Atualizando a base:

PM> add-migration comitx
PM> update-database 

Deploy Heroku:

docker build -t vacina-api .
heroku container:login
heroku container:push web -a carter-api
heroku container:release web -a minha-vacina


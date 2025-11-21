using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using Setup.Common;
using Setup.Nosql.Repositories;
using System;
using System.Threading.Tasks;
using DotNetEnv;

BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

Env.Load();



var connectionString = Env.GetString("MONGO_CONNECTION_STRING");

var client = new MongoClient(connectionString);

var database = client.GetDatabase("Lb3");

var computerRepo = new MongoRepository<Computer>(database, "Computers");

Console.WriteLine("Підключення успіше");
var newComp = Computer.GenerateRandom();
await computerRepo.AddAsync(newComp);

Console.WriteLine($"Додано комп'ютер: {newComp.Name} ({newComp.Id})");


var found = await computerRepo.GetByIdAsync(newComp.Id);
if (found != null)
    Console.WriteLine($"Знайдено в БД: {found.Name}");
else
    Console.WriteLine("Не знайдено документ!");

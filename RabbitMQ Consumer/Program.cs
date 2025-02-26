﻿
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

channel.QueueDeclare(
     queue: "hello",
     durable: false,
     autoDelete: false,
     exclusive: false,
     arguments: null
);

Console.WriteLine("[*] Waiting for messages");

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"[*] Received {message}");
};

channel.BasicConsume(queue:"hello",autoAck:true,consumer:consumer);

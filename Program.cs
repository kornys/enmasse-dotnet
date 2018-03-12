﻿using System;
using Amqp;

namespace dotnet_pokus
{
    public class Program
    {
        public static void Main(string[] args)
        {
            String url = (args.Length > 0) ? args[0] : "amqps://127.0.0.1:443";
            String address = (args.Length > 1) ? args[1] : "myqueue";

            
            Trace.TraceLevel = TraceLevel.Frame;
            Trace.TraceListener = (l, f, a) => Console.WriteLine(
                        DateTime.Now.ToString("[hh:mm:ss.fff]") + " " + string.Format(f, a));

	    Connection.DisableServerCertValidation = true;
            Connection connection = new Connection(new Address(url));
            Session session = new Session(connection);
            SenderLink sender = new SenderLink(session, "test-sender", address);

            Message messageSent = new Message("Test Message");
            sender.Send(messageSent);

            ReceiverLink receiver = new ReceiverLink(session, "test-receiver", address);
            Message messageReceived = receiver.Receive(TimeSpan.FromSeconds(2));
            Console.WriteLine(messageReceived.Body);
            receiver.Accept(messageReceived);

            sender.Close();
            receiver.Close();
            session.Close();
            connection.Close();
        }
    }
}

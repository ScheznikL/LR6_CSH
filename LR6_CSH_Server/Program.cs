//#define TRACE
using System;
using System.Threading;
using System.Net;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LR6_CSH_Server
{
    class Program
    {

        private static HttpListener httpListener;
        private static string token;
        static void Main()
        {
            FileConfig.ReadUserInfo();
            Task.WaitAll(Task.Run(() => Listen()));
        }

        private static void StartServer()
        {
            try
            {
                httpListener = new HttpListener();
                httpListener.Prefixes.Add("http://*:8080/");

                // Specify the authentication delegate.

                // httpListener.AuthenticationSchemes = AuthenticationSchemes.Anonymous | AuthenticationSchemes.Basic;

                //httpListener.AuthenticationSchemeSelectorDelegate = 
                //    new AuthenticationSchemeSelector(AuthenticationSchemeForClient);

                httpListener.Start();
                Console.WriteLine("HTTP Server was stared...");
            }

            catch
            {
                Console.WriteLine("Smth went wrong.\nRerun Server with higher rights...");
            }
        }

        //static AuthenticationSchemes AuthenticationSchemeForClient(HttpListenerRequest request)
        //{
        //    Console.WriteLine("Client authentication protocol selection in progress...");
        //    // Do not authenticate local machine requests.

        //    var res1 = request.IsAuthenticated;
        //    var res2 = request.GetClientCertificate();
        //    var res3 = request.ClientCertificateError;
        //    if (request.RemoteEndPoint.Address.Equals(IPAddress.Broadcast) || request.IsLocal)
        //    {
        //        return AuthenticationSchemes.None;
        //    }
        //    else
        //    {
        //        return AuthenticationSchemes.Basic;
        //    }
        //}

        public static async Task ProcessRequestAsync(HttpListenerContext context)
        {
            DisplayWebHeaderCollection(context.Request); 

            if (context.Request.HttpMethod == "POST" && context.Request.Url.AbsolutePath == "/reg-user")
            {
                int after;
                int countbefore = after = UserOnServer.UsersOnServer.Count;
                try
                {
                    using (Stream responseStream = context.Request.InputStream)
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            string json = reader.ReadToEnd();
                            await JSONOrganaizer.DeserializeUsersFromStringToFile(json, token);
                            context.Response.StatusCode = 200;
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine($"Cose forbidden sent to {token}");
                    context.Response.StatusCode = 403;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while processing reg-user request: " + ex.Message);
                    context.Response.StatusCode = 500;
                }
                finally
                {
                    context.Response.Close();
                }
            }
            else if (context.Request.HttpMethod == "GET" && context.Request.Url.AbsolutePath == "/get-users")
            {
                try
                {
                    if (UserOnServer.UsersPack.Count > 1)
                    {
                        string jsonResponse = JsonSerializer.Serialize(UserOnServer.UsersPack);
                        byte[] responseBytes = Encoding.UTF8.GetBytes(jsonResponse);
                        context.Response.StatusCode = 200;
                        context.Response.ContentType = "application/json";
                        context.Response.ContentEncoding = Encoding.UTF8;
                        context.Response.ContentLength64 = responseBytes.Length;
                        context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                    }
                    else
                    {
                        context.Response.StatusCode = 204;
                        Console.WriteLine($"User {token} asked for accaunts but no other user registred yet.");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while processing get-users request: " + ex.Message);
                    context.Response.StatusCode = 500;
                }
                finally
                {
                    context.Response.Close();
                }
            }
            else if (context.Request.HttpMethod == "POST" && context.Request.Url.AbsolutePath == "/send-message")
            {
                try
                {
                    using (Stream responseStream = context.Request.InputStream)
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            string json = reader.ReadToEnd();
                            UserPack userFromResponse = JsonSerializer.Deserialize<UserPack>(json);
                            UserMessage message = new UserMessage(userFromResponse.Login,
                                                                  userFromResponse.Resiver,
                                                                  $"[{DateTime.Now.ToShortTimeString()}]|{userFromResponse.Login}|\n{ userFromResponse.Message }");

                            Console.WriteLine($"User {userFromResponse.Login} message was recieved by server.");
                        }
                    }
                    context.Response.StatusCode = 200;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while processing send-message request: " + ex.Message);
                    context.Response.StatusCode = 500;
                }
                finally
                {
                    context.Response.Close();
                }
            }
            else if (context.Request.HttpMethod == "GET" && context.Request.Url.AbsolutePath == "/get-messages")
            {
                try
                {
                    Console.WriteLine($"User by token:{token} ask for messages.");
                    var massegesforuser = UserMessage.FindMessages(token);
                    if (massegesforuser)
                    {
                        string jsonAnswer = JsonSerializer.Serialize(UserOnServer.UsersPack);
                        byte[] responseBytes = Encoding.UTF8.GetBytes(jsonAnswer);
                        context.Response.StatusCode = 200;
                        context.Response.ContentType = "application/json";
                        context.Response.ContentEncoding = Encoding.UTF8;
                        context.Response.ContentLength64 = responseBytes.Length;
                        context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                        UserMessage.ClearePotentiallyReadMes(token);
                    }
                    else
                    {
                        context.Response.StatusCode = 204;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while processing get-messages request: " + ex.Message);

                    context.Response.StatusCode = 500;
                }
                finally
                {
                    context.Response.Close();
                }
            }
            else if (context.Request.HttpMethod == "POST" && context.Request.Url.AbsolutePath == "/auth-user")
            {
                try
                {
                    UserPack userFromResponse;
                    using (Stream responseStream = context.Request.InputStream)
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            string json = reader.ReadToEnd();
                            userFromResponse = JsonSerializer.Deserialize<UserPack>(json);
                        }
                    }

                    var isPresent = UserOnServer.CheckUsersPerenceAndPassword(userFromResponse);
                    if (isPresent == 200)
                    {
                        string jsonResponse = JsonSerializer.Serialize(UserOnServer.UsersOnServer);
                        byte[] responseBytes = Encoding.UTF8.GetBytes(jsonResponse);
                        context.Response.StatusCode = 200;
                        context.Response.ContentType = "application/json";
                        context.Response.ContentEncoding = Encoding.UTF8;
                        context.Response.ContentLength64 = responseBytes.Length;
                        context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                        Console.WriteLine($"User {userFromResponse.Login} authoraized successfully.");
                    }
                    else if (isPresent == 401)
                    {
                        context.Response.StatusCode = 401;
                    }
                    else
                    {
                        context.Response.StatusCode = 401;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while processing auth-user request: " + ex.Message);
                    context.Response.StatusCode = 500;
                }
                finally
                {
                    context.Response.Close();
                }
            }
            else
            {
                context.Response.StatusCode = 404;
                Console.WriteLine("Server send the 404 not found code\n");
                context.Response.Close();
            }
        }

        public static void DisplayWebHeaderCollection(HttpListenerRequest request)
        {
            System.Collections.Specialized.NameValueCollection headers = request.Headers;
            foreach (string key in headers.AllKeys)
            {
                if (key == "Authorization")
                {
                    string[] values = headers.GetValues(key);
                    if (values.Length > 0)
                    {
                        foreach (string value in values)
                        {
                            var splited = value.Split(' ');
                            token = splited[1];
                           // Console.WriteLine("Token registred", value);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No token value found");
                    }
                }
            }
        }

        public async static Task Listen()
        {
            var requests = new List<Task>();
            StartServer();
            requests.Add(httpListener.GetContextAsync());
            while (true)
            {
                Task t = await Task.WhenAny(requests);
                requests.Remove(t);
                if (t is Task<HttpListenerContext>)
                {
                    var context = (t as Task<HttpListenerContext>).Result;
                    requests.Add(ProcessRequestAsync(context));
                    requests.Add(httpListener.GetContextAsync());
                }
            }
        }


    }
}

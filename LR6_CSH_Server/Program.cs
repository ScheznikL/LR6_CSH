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
                //httpListener.Prefixes.Add("http://localhost:8080/");
                httpListener.Prefixes.Add("http://*:8080/");

                // Specify the authentication delegate.

                httpListener.AuthenticationSchemes = /*AuthenticationSchemes.Anonymous | */AuthenticationSchemes.Basic;

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

        static AuthenticationSchemes AuthenticationSchemeForClient(HttpListenerRequest request)
        {
            Console.WriteLine("Client authentication protocol selection in progress...");
            // Do not authenticate local machine requests.

            var res1 = request.IsAuthenticated;
            var res2 = request.GetClientCertificate();

            var res3 = request.ClientCertificateError;
            //////
            if (request.RemoteEndPoint.Address.Equals(IPAddress.Broadcast) || request.IsLocal)
            {
                return AuthenticationSchemes.None;
            }
            else
            {
                return AuthenticationSchemes.Basic;
            }
        }



        //static void runHTTPServer()
        //{
        //    try
        //    {
        //        httpListener.Start();
        //        Console.WriteLine("HTTP server started. Listening for incoming requests...");

        //        while (true)
        //        {
        //            HttpListenerContext context = httpListener.GetContext();

        //            if (context.Request.HttpMethod == "POST" && context.Request.Url.AbsolutePath == "/reg-user")
        //            {
        //                try
        //                {
        //                    using (Stream responseStream = context.Request.InputStream)
        //                    {
        //                        using (var reader = new StreamReader(responseStream))
        //                        {
        //                            string json = reader.ReadToEnd();
        //                            JSONOrganaizer.DeserializeUsersFromStringToList(json);
        //                            context.Response.StatusCode = 200;
        //                        }
        //                    }
        //                }
        //                catch (HttpListenerException winsevEx)
        //                {
        //                    Console.WriteLine("An WINDOWS 7 Unfortunatelly ad ** using staff" + winsevEx.Message);
        //                    Console.ReadLine();
        //                    context.Response.StatusCode = 500;
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine("An error occurred while processing the request: " + ex.Message);
        //                    Console.ReadLine();
        //                    context.Response.StatusCode = 500;
        //                }
        //                finally
        //                {
        //                    context.Response.Close();
        //                    // httpListener.Stop();
        //                }
        //            }

        //            else
        //            {
        //                context.Response.StatusCode = 404;
        //                context.Response.Close();
        //                // httpListener.Stop();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("An error occurred: " + ex.Message);
        //    }
        //    //finally
        //    //{
        //    //    httpListener.Stop();
        //    //}
        //}

        //static void RunHTTPServer()
        //{
        //    try
        //    {
        //        httpListener.Start();
        //        Console.WriteLine("HTTP Server was stared...");
        //        FileConfig.ReadUserInfo();

        //        IAsyncResult result = httpListener.BeginGetContext(new AsyncCallback(ListenerCallback), httpListener);
        //        Console.WriteLine("Waiting for request to be processed asyncronously.");
        //        //                result.AsyncWaitHandle.WaitOne();

        //        httpListener.Stop();
        //        Console.WriteLine("HTTP Server has stoped\nType smth to continue...");
        //        Console.ReadLine();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("An error occurred: " + ex.Message);
        //        Console.ReadLine();
        //    }
        //    finally
        //    {
        //        httpListener.Close();
        //        // Console.ReadLine();
        //    }
        //}

        //public static void ListenerCallback(IAsyncResult result)
        //{
        //    HttpListener listener = (HttpListener)result.AsyncState;
        //    // Call EndGetContext to complete the asynchronous operation.
        //    HttpListenerContext context = listener.EndGetContext(result);

        //    if (context.Request.HttpMethod == "POST" && context.Request.Url.AbsolutePath == "/reg-user")
        //    {
        //        try
        //        {
        //            using (Stream responseStream = context.Request.InputStream)
        //            {
        //                using (var reader = new StreamReader(responseStream))
        //                {
        //                    string json = reader.ReadToEnd();
        //                    JSONOrganaizer.DeserializeUsersFromStringToList(json);
        //                }
        //            }
        //            context.Response.StatusCode = 200;
        //            FileConfig.UpdateJSONfile();
        //            Console.WriteLine("JSON File updated.");
        //        }
        //        catch (HttpListenerException winsevEx)
        //        {
        //            Console.WriteLine("An WINDOWS 7 Unfortunatelly ad ** using staff" + winsevEx.Message);
        //            Console.ReadLine();
        //            context.Response.StatusCode = 500;
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("An error occurred while processing the request: " + ex.Message);
        //            Console.ReadLine();
        //            context.Response.StatusCode = 500;
        //        }
        //        finally
        //        {
        //            context.Response.Close();
        //            // Console.ReadLine();
        //        }
        //    }
        //    else if (context.Request.HttpMethod == "GET" && context.Request.Url.AbsolutePath == "/get-users")
        //    {
        //        try
        //        {
        //            string jsonResponse = JsonSerializer.Serialize(UserOnServer.UsersOnServer);
        //            byte[] responseBytes = Encoding.UTF8.GetBytes(jsonResponse);

        //            context.Response.StatusCode = 200;
        //            context.Response.ContentType = "application/json";
        //            context.Response.ContentEncoding = Encoding.UTF8;
        //            context.Response.ContentLength64 = responseBytes.Length;

        //            context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("An error occurred while processing the request: " + ex.Message);
        //            Console.ReadLine();
        //            context.Response.StatusCode = 500;
        //        }
        //        finally
        //        {
        //            context.Response.Close();
        //            // Console.ReadLine();
        //        }
        //    }
        //    else if (context.Request.HttpMethod == "POST" && context.Request.Url.AbsolutePath == "/send-message")
        //    {
        //        try
        //        {
        //            using (Stream responseStream = context.Request.InputStream)
        //            {
        //                using (var reader = new StreamReader(responseStream))
        //                {
        //                    string json = reader.ReadToEnd();
        //                    UserPack userFromResponse = JsonSerializer.Deserialize<UserPack>(json);
        //                    UserMessage message = new UserMessage(userFromResponse.Login,
        //                                                            userFromResponse.Resiver,
        //                                                            userFromResponse.Message);
        //                    lock (UserMessage.Locker)
        //                    {
        //                        UserMessage.UserMessages.Add(message);
        //                    }
        //                    Console.WriteLine($"User {userFromResponse.Login} message was recieved by server.");

        //                }
        //            }
        //            context.Response.StatusCode = 200;
        //        }
        //        catch (HttpListenerException winsevEx)
        //        {
        //            Console.WriteLine("An WINDOWS 7 Unfortunatelly ad ** using staff" + winsevEx.Message);
        //            Console.ReadLine();
        //            context.Response.StatusCode = 500;
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("An error occurred while processing the request: " + ex.Message);
        //            Console.ReadLine();
        //            context.Response.StatusCode = 500;
        //        }
        //        finally
        //        {
        //            context.Response.Close();
        //            // Console.ReadLine();
        //        }
        //    }
        //    else if (context.Request.HttpMethod == "GET" && context.Request.Url.AbsolutePath == "/get-messages")
        //    {
        //        try
        //        {
        //            UserPack userFromResponse;
        //            using (Stream responseStream = context.Request.InputStream)
        //            {
        //                using (var reader = new StreamReader(responseStream))
        //                {
        //                    string json = reader.ReadToEnd();
        //                    userFromResponse = JsonSerializer.Deserialize<UserPack>(json);
        //                    Console.WriteLine($"User {userFromResponse.Login} ask for messages.");
        //                }
        //            }
        //            var massegesforuser = UserMessage.FindMessages(userFromResponse);
        //            if (massegesforuser != null)
        //            {
        //                string jsonAnswer = JsonSerializer.Serialize(UserMessage.FindMessages(userFromResponse));
        //                byte[] responseBytes = Encoding.UTF8.GetBytes(jsonAnswer);

        //                context.Response.StatusCode = 200;
        //                context.Response.ContentType = "application/json";
        //                context.Response.ContentEncoding = Encoding.UTF8;
        //                context.Response.ContentLength64 = responseBytes.Length;

        //                context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
        //            }
        //            else
        //            {
        //                context.Response.StatusCode = 204;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("An error occurred while processing the request: " + ex.Message);
        //            Console.ReadLine();
        //            context.Response.StatusCode = 500;
        //        }
        //        finally
        //        {
        //            context.Response.Close();
        //            // Console.ReadLine();
        //        }
        //    }
        //    else if (context.Request.HttpMethod == "POST" && context.Request.Url.AbsolutePath == "/auth-user")
        //    {
        //        try
        //        {
        //            UserPack userFromResponse;
        //            using (Stream responseStream = context.Request.InputStream)
        //            {
        //                using (var reader = new StreamReader(responseStream))
        //                {
        //                    string json = reader.ReadToEnd();
        //                    userFromResponse = JsonSerializer.Deserialize<UserPack>(json);
        //                }
        //            }

        //            var isPresent = UserOnServer.CheckUsersPerence(userFromResponse);
        //            if (isPresent)
        //            {
        //                string jsonResponse = JsonSerializer.Serialize(UserOnServer.UsersOnServer);
        //                byte[] responseBytes = Encoding.UTF8.GetBytes(jsonResponse);

        //                context.Response.StatusCode = 200;
        //                context.Response.ContentType = "application/json";
        //                context.Response.ContentEncoding = Encoding.UTF8;
        //                context.Response.ContentLength64 = responseBytes.Length;

        //                context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
        //            }
        //            else
        //            {
        //                context.Response.StatusCode = 404;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("An error occurred while processing the request: " + ex.Message);
        //            Console.ReadLine();
        //            context.Response.StatusCode = 500;
        //        }
        //        finally
        //        {
        //            context.Response.Close();
        //            //Console.ReadLine();
        //        }
        //    }
        //    else
        //    {
        //        context.Response.StatusCode = 404;
        //        Console.WriteLine("Server send the 404 not found code \nType anthythig to continue...");
        //        context.Response.Close();
        //        Console.ReadLine();
        //    }
        //}

        public static async Task ProcessRequestAsync(HttpListenerContext context)
        {
            DisplayWebHeaderCollection(context.Request); //WHO

            if (context.Request.HttpMethod == "POST" && context.Request.Url.AbsolutePath == "/reg-user")
            {
                //Find IP  
                try
                {
                    using (Stream responseStream = context.Request.InputStream)
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            string json = reader.ReadToEnd();
                            await JSONOrganaizer.DeserializeUsersFromStringToFile(json, token);
                        }
                    }
                    // var isPresent = UserOnServer.CheckUsersPerenceAndPassword(userFromResponse);
                    context.Response.StatusCode = 200;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while processing reg-user request: " + ex.Message);
                    Console.ReadLine();
                    context.Response.StatusCode = 500;
                }
                finally
                {
                    context.Response.Close();
                    // Console.ReadLine();
                }
            }
            else if (context.Request.HttpMethod == "GET" && context.Request.Url.AbsolutePath == "/get-users")
            {
                try
                {


                    string jsonResponse = JsonSerializer.Serialize(UserOnServer.UsersOnServer);
                    byte[] responseBytes = Encoding.UTF8.GetBytes(jsonResponse);

                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";
                    context.Response.ContentEncoding = Encoding.UTF8;
                    context.Response.ContentLength64 = responseBytes.Length;

                    context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while processing get-users request: " + ex.Message);
                    Console.ReadLine();
                    context.Response.StatusCode = 500;
                }
                finally
                {
                    context.Response.Close();
                    // Console.ReadLine();
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
                                                                  $"[{DateTime.Now}]:{userFromResponse.Login}:\n{ userFromResponse.Message }");
                            lock (UserMessage.Locker)
                            {
                                UserMessage.UserMessages.Add(message);
                            }
                            Console.WriteLine($"User {userFromResponse.Login} message was recieved by server.");

                        }
                    }
                    context.Response.StatusCode = 200;
                }
                catch (HttpListenerException winsevEx)
                {
                    Console.WriteLine("An WINDOWS 7 Unfortunatelly ad ** using staff" + winsevEx.Message);
                    Console.ReadLine();
                    context.Response.StatusCode = 500;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while processing send-message request: " + ex.Message);
                    Console.ReadLine();
                    context.Response.StatusCode = 500;
                }
                finally
                {
                    context.Response.Close();
                    // Console.ReadLine();
                }
            }
            else if (context.Request.HttpMethod == "GET" && context.Request.Url.AbsolutePath == "/get-messages")
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
                            Console.WriteLine($"User {userFromResponse.Login} ask for messages.");
                        }

                        var massegesforuser = UserMessage.FindMessages(userFromResponse);
                        if (massegesforuser != null)
                        {
                            string jsonAnswer = JsonSerializer.Serialize(massegesforuser);
                            byte[] responseBytes = Encoding.UTF8.GetBytes(jsonAnswer);

                            context.Response.StatusCode = 200;
                            context.Response.ContentType = "application/json";
                            context.Response.ContentEncoding = Encoding.UTF8;
                            context.Response.ContentLength64 = responseBytes.Length;

                            context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                        }
                        else
                        {
                            context.Response.StatusCode = 204;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while processing get-messages request: " + ex.Message);
                    Console.ReadLine();
                    context.Response.StatusCode = 500;
                }
                finally
                {
                    context.Response.Close();
                    // Console.ReadLine();
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
                    Console.ReadLine();
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
                Console.WriteLine("Server send the 404 not found code\n");/*Type anthythig to continue...*/
                context.Response.Close();
                // Console.ReadLine();
            }
        }


        // Displays the header information that accompanied a request.
        public static void DisplayWebHeaderCollection(HttpListenerRequest request)
        {
            System.Collections.Specialized.NameValueCollection headers = request.Headers;
            // Get each header and display each value.

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
                            Console.WriteLine("Token registred", value);
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
            StartServer();
            //HttpListener listener = new HttpListener(); //up all
            //listener.Prefixes.Add(prefix);
            //listener.Start();

            var requests = new List<Task>();
            // for (int i = 0; i < maxConcurrentRequests; i++)
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

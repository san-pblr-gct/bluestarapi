using System;
using System.IO;
using System.Net;
using System.Web;
namespace ss_api.Models {
    public sealed class SSAPIClient {

        private string baseUrl;
        private string consumerKey;
        private string consumerSecret;

        public SSAPIClient (string baseUrl, string consumerKey, string consumerSecret) {
            this.baseUrl = baseUrl;
            this.consumerKey = consumerKey;
            this.consumerSecret = consumerSecret;
            HttpWebRequest request1 = WebRequest.Create (baseUrl) as HttpWebRequest;
            request1.Timeout = (4 * 60 * 1000);

        }

        private string formHeader (string requestUrl, string methodType) {
            OAuthBase oauthBase = new OAuthBase ();
            string normalisedUrl = string.Empty;
            string normalisedParams = string.Empty;
            string authHeader = string.Empty;
            string timeStamp = oauthBase.GenerateTimeStamp ();
            string nonce = oauthBase.GenerateNonce ();
            string requestWithAuth = oauthBase.GenerateSignature (new Uri (requestUrl), this.consumerKey, this.consumerSecret,
                "", "", methodType, timeStamp, nonce, OAuthBase.SignatureTypes.HMACSHA1, out normalisedUrl, out normalisedParams, out authHeader);
            string finalAuthHeader = "OAuth oauth_nonce=\"" + nonce + "\",oauth_consumer_key=\"" + consumerKey + "\",oauth_signature_method=\"HMAC-SHA1\",oauth_timestamp=\"" +
                timeStamp + "\",oauth_version=\"1.0\",oauth_signature=\"" + HttpUtility.UrlEncode (requestWithAuth) + "\"";
            return finalAuthHeader;

        }

        private string invokeGetRequest (string requestUrl) {
            string completeUrl = baseUrl + requestUrl;
            string header = "";
            try {
                HttpWebRequest request1 = WebRequest.Create (completeUrl) as HttpWebRequest;
                request1.ContentType = @"application/json";
                request1.Method = @"GET";
                header = formHeader (completeUrl, "GET");
                request1.Headers.Add (HttpRequestHeader.Authorization, header);
                //request1.Timeout = (4 * 60 * 1000);

                HttpWebResponse httpWebResponse = (HttpWebResponse) request1.GetResponse ();
                StreamReader reader = new
                StreamReader (httpWebResponse.GetResponseStream ());
                string responseString = reader.ReadToEnd ();
                return responseString;
            } catch (WebException ex) {
                //reading the custom messages sent by the server
                using (var reader = new StreamReader (ex.Response.GetResponseStream ())) {
                    return reader.ReadToEnd ();
                }
            } catch (Exception ex) {
                return "Failed with exception message:" + ex.Message;
            }

        }

        private string invokePostRequest (string requestUrl, string requestBody) {

            string completeUrl = baseUrl + requestUrl;
            try {

                HttpWebRequest request = WebRequest.Create (completeUrl) as HttpWebRequest;
                request.ContentType = @"application/json";
                request.Method = @"POST";
                //request.Timeout=(4 * 60 * 1000);

                request.Headers.Add (HttpRequestHeader.Authorization, formHeader (completeUrl, "POST"));
                //requestWithAuth.Context.GenerateOAuthParametersForHeader() //alternative did not work

                StreamWriter requestWriter = new StreamWriter (request.GetRequestStream ());
                requestWriter.Write (requestBody);
                requestWriter.Close ();

                //request.Headers.Add(HttpRequestHeader.ContentLength, requestBody.Length.ToString());

                HttpWebResponse webResponse = (HttpWebResponse) request.GetResponse ();

                StreamReader responseReader = new StreamReader (webResponse.GetResponseStream ());
                return responseReader.ReadToEnd ();

            } catch (WebException ex) {
                //reading the custom messages sent by the server
                using (var reader = new StreamReader (ex.Response.GetResponseStream ())) {
                    return reader.ReadToEnd ();
                }
            } catch (Exception ex) {
                return ex.Message + ex.InnerException + "--error" + "requestString:::::::::::: " + requestBody;
            }

        }

        public string getDummy () {
            return invokeGetRequest ("/dummy");

        }
        public string getAllSources () {
            return invokeGetRequest ("/sources");

        }
        public string getAllDestinations (string sourceId) {

            return invokeGetRequest ("/destinations?source=" + sourceId);

        }

        public string getAvailableTrips (string sourceId, string DestinationId, string DateOfJourny)

        {

            return invokeGetRequest ("/availabletrips?source=" + sourceId + "&destination=" + DestinationId + "&doj=" + DateOfJourny);

        }

        public string getTripDetails (string tripId) {

            return invokeGetRequest ("/tripdetails?id=" + tripId);
        }

        /************ The below API has been deprecated ************/

        //public string getBoardingPoint(string bordingPointString)
        //{

        //        return invokeGetRequest("/boardingPoint?id=" + bordingPointString);

        //}

        public string getBoardingPoint (string bordingPointString, string tripId) {

            return invokeGetRequest ("/boardingPoint?id=" + bordingPointString + "&tripId=" + tripId);

        }

        public string getTicket (string tinString) {

            return invokeGetRequest ("/ticket?tin=" + tinString);

        }

        public string getCancellationData (string cancellationTinString) {

            return invokeGetRequest ("/cancellationdata?tin=" + cancellationTinString);

        }

        public string blockTicket (string requestString) {

            return invokePostRequest ("/blockTicket", requestString);
        }

        public string bookTicket (string tentativeresponsekey) {

            return invokePostRequest ("/bookticket?blockKey=" + tentativeresponsekey, "");
        }

        public string cancelTicket (string makeCancellationRequest) {

            return invokePostRequest ("/cancelticket", makeCancellationRequest);

        }

        public string checkTicketBooked (string checkTicketBookedRequest) {

            return invokeGetRequest ("/checkBookedTicket?blockKey=" + checkTicketBookedRequest);

        }
    }
}
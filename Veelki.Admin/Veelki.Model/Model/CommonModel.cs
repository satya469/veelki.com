using System;
using System.Collections.Generic;

namespace Veelki.Models.Model
{
    public class CommonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CommonReturnResponse
    {
        public Boolean IsSuccess { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }

    public class SeriesReturnResponse
    {
        public string message { get; set; }
        public int code { get; set; }
        public bool error { get; set; }
        public List<SeriesDataByApi> data { get; set; }
        public long currentTime { get; set; }
    }

    public class MatchesReturnResponse
    {
        public string message { get; set; }
        public int code { get; set; }
        public bool error { get; set; }
        public List<MatchesDataByApi> data { get; set; }
        public long currentTime { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AvailableToBack
    {
        public double price { get; set; }
        public double size { get; set; }
    }

    public class AvailableToLay
    {
        public double price { get; set; }
        public double size { get; set; }
    }

    public class Ex
    {
        public List<AvailableToBack> availableToBack { get; set; }
        public List<AvailableToLay> availableToLay { get; set; }
    }

    public class Runner
    {
        public int selectionId { get; set; }
        public int handicap { get; set; }
        public string status { get; set; }
        public double lastPriceTraded { get; set; }
        public double totalMatched { get; set; }
        public int adjustmentFactor { get; set; }
        public Ex ex { get; set; }
    }

    public class MatchReturnResponseNew
    {
        public string marketId { get; set; }
        public DateTime updateTime { get; set; }
        public string status { get; set; }
        public bool inplay { get; set; }
        public double totalMatched { get; set; }
        public List<Runner> runners { get; set; }
    }



    public class EventReturnResponse
    {
        public List<List<MatchOddsVM>> t1 { get; set; }
        //public List<object> t2 { get; set; }
        //public object t3 { get; set; }
        //public object t4 { get; set; }
        public string delayed { get; set; }
    }

    public class PriorityStatus
    {
        public int PriorityId { get; set; }
        public string Priority { get; set; }
        public string PriorityColourCode { get; set; }
    }

    public class SportsData
    {
        public string key { get; set; }
        public string group { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool active { get; set; }
        public bool has_outrights { get; set; }
    }

    public class SportsSettings
    {
        public string sportId { get; set; }
        public string sportName { get; set; }
        public string iconUrl { get; set; }
        public bool highlight { get; set; }
        public int sequence { get; set; }
        public bool popular { get; set; }
        public bool status { get; set; }
        public bool others { get; set; }
    }

    public class SeriesDataByApi
    {
        public long SeriesId { get; set; }
        public string SeriesName { get; set; }
    }

    public class MatchesDataByApi
    {
        public long eventId { get; set; }
        public string eventName { get; set; }
        public DateTime eventDate { get; set; }
        public long SeriesId { get; set; }
        public string SeriesName { get; set; }
        public string marketId { get; set; }
        public string MainTvurl { get; set; }
        public int market_runner_count { get; set; }
    }

    public class MatchOddsVM
    {
        public string mid { get; set; }
        public string mstatus { get; set; }
        public string mname { get; set; }
        public string iplay { get; set; }
        public string sid { get; set; }
        public string nat { get; set; }
        public string b1 { get; set; }
        public string bs1 { get; set; }
        public string b2 { get; set; }
        public string bs2 { get; set; }
        public string b3 { get; set; }
        public string bs3 { get; set; }
        public string l1 { get; set; }
        public string ls1 { get; set; }
        public string l2 { get; set; }
        public string ls2 { get; set; }
        public string l3 { get; set; }
        public string ls3 { get; set; }
        public string status { get; set; }
        public string srno { get; set; }
        public string gtype { get; set; }
        public string utime { get; set; }
    }

    public class Outcome
    {
        public string name { get; set; }
        public double price { get; set; }
    }

    public class Market
    {
        public string key { get; set; }
        public List<Outcome> outcomes { get; set; }
    }

    public class Bookmaker
    {
        public string key { get; set; }
        public string title { get; set; }
        public DateTime last_update { get; set; }
        public List<Market> markets { get; set; }
    }

    public class MatchList
    {
        public string id { get; set; }
        public string sport_key { get; set; }
        public string sport_title { get; set; }
        public DateTime commence_time { get; set; }
        public string home_team { get; set; }
        public string away_team { get; set; }
    }

    public class MatchOdds
    {
        public string id { get; set; }
        public string sport_key { get; set; }
        public string sport_title { get; set; }
        public DateTime commence_time { get; set; }
        public string home_team { get; set; }
        public string away_team { get; set; }
        public List<Bookmaker> bookmakers { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Datum
    {
        public int srno { get; set; }
        public string eventId { get; set; }
        public string eventName { get; set; }
        public DateTime eventDate { get; set; }
        public int EventTypeId { get; set; }
        public string EventTypeName { get; set; }
        public string SeriesId { get; set; }
        public string SeriesName { get; set; }
        public string marketId { get; set; }
        public string marketName { get; set; }
        public string marketType { get; set; }
        public int selectionId1 { get; set; }
        public string runnerName1 { get; set; }
        public int selectionId2 { get; set; }
        public string runnerName2 { get; set; }
        public int selectionId3 { get; set; }
        public string runnerName3 { get; set; }
        public int selectionId4 { get; set; }
        public string runnerName4 { get; set; }
        public int selectionId5 { get; set; }
        public string runnerName5 { get; set; }
        public int selectionId6 { get; set; }
        public string runnerName6 { get; set; }
        public int selectionId7 { get; set; }
        public string runnerName7 { get; set; }
        public int selectionId8 { get; set; }
        public string runnerName8 { get; set; }
        public int selectionId9 { get; set; }
        public string runnerName9 { get; set; }
        public int selectionId10 { get; set; }
        public string runnerName10 { get; set; }
        public int selectionId11 { get; set; }
        public string runnerName11 { get; set; }
        public int selectionId12 { get; set; }
        public string runnerName12 { get; set; }
        public string market_runner_json { get; set; }
        public string match_type { get; set; }
        public DateTime startDate { get; set; }
        public string scoreboard_id { get; set; }
        public string new_runner_json { get; set; }
        public int market_runner_count { get; set; }
        public object MainTvurl { get; set; }
    }

    public class TeamNameResponse
    {
        public string message { get; set; }
        public int code { get; set; }
        public bool error { get; set; }
        public List<Datum> data { get; set; }
        public long currentTime { get; set; }
    }

    public class TeamSelectionId
    {
        public long selectionId { get; set; }
        public string teamName { get; set; }
    }

    public class TeamAmount
    {
        public long selectionId { get; set; }
        public double amount { get; set; }
    }

    public class TeamNameAmount
    {
        public string selectionName { get; set; }
        public double amount { get; set; }
    }
}

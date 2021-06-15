# LeagueTrack

[![Build status](https://ci.appveyor.com/api/projects/status/1cwqfwrd5y7vt85q/branch/master?svg=true)](https://ci.appveyor.com/project/dawidkacprzak/leaguetrack/branch/master)

C# Based platform for tracking players.  
 [GPL 3.0 License](LICENSE)

✔️ - Done
⚙️ - WIP
❌ - TODO
| Feature | Status |
| ------------- |:-------------:|
| Riot API Wrapper | ✔️ |
| Riot API Wrapper rate limiter | ✔️ |
| Rest API | ⚙️ |
| Mobile application | ❌ |
| Web application | ❌ |
| Desktop application | ❌ |

---

## Idea

The idea of this project is to gain ability for easy tracking other player's activity.
MVP must be built with following assumptions:

- One account for all platforms
- No need to connect league account
- No personal data needed
- Full open source
- Easy insert any player to account's dictionary
- Track player's games activity like game history, notifications about started games
- Easy op.gg, porofessor.gg check
- Analyze each game progress like builds, runes, items etc.

`Collaborate.`

---

## API Wrapper Example

[![NuGet version](https://badge.fury.io/nu/LeagueTrack.ApiWrapper.svg)](https://badge.fury.io/nu/LeagueTrack.ApiWrapper)

To use api wrapper you have to create new Api instance

```cs
Api instance = new Api(<RIOT APP KEY>, <OneSecLimit>, <TwoMinLimit>, ELocation.EUNE);
```

Just insert your `api key` from RIOT Developer portal https://developer.riotgames.com/

After registration your project is restricted by limited `api rate limit`.

From project settings (look at riot developer portal -> app register) check your rate limits and put corresponding values to `OneSecLimit` and `TwoMinLimit` fields.

`ELocation` enum stands for game server.

<br>

Create request object with following statement

```cs
RiotRequest request = (RiotRequest)instance.SummonerV4.ByName("Rekurencja");
//Or
IRequest request = instance.SummonerV4.ByName("Rekurencja");
```

`SummonerV4` stands for **endpoint** and **ByName** is just method.

`RiotRequest` object is request wrapper which contain your api key, server location, header and query parameters, URL address and method path.

You can also check final HTTP link from `HttpAddress` field.  
<br>

### Requests

Requests can be called by `RiotRequestSender` object.

Creation process is done by builder pattern using `RequestSenderDirector` class.

#### Example

```cs
Api api = new Api(<RIOT APP KEY>, <OneSecLimit>, <TwoMinLimit>, ELocation.EUNE);
RequestSenderDirector director = new RequestSenderDirector();
director.builder = new RiotRequestSenderBuilder(<optional retry count>);
director.Construct();
IRequestSender sender = director.builder.GetRequestSender();
//Constructed IRequestSender can be used multiple times. You dont have to contruct it every request

RiotRequest r = (RiotRequest)api.SummonerV4.ByName("Rekurencja");
//Api request preparing

IResponse getSummonerByNameResponse = await sender.GetAsync(r);
//Api call

Summoner summoner = JsonConvert.DeserializeObject<Summoner>(getSummonerByNameResponse.GetResponseContent());
//Response mapping to concrete object

```

### Full list of **endpoints** and **methods**

| Endpoint     |                      Method                       | Response Mapping Object |
| ------------ | :-----------------------------------------------: | :---------------------: |
| `SummonerV4` |              `ByName(Summoner_Name)`              |       `Summoner`        |
| `SummonerV4` |         `ByAccount(Encrypted_account_id)`         |       `Summoner`        |
| `SummonerV4` |            `ByPuuid(Encrypted_Puuid)`             |       `Summoner`        |
| `SummonerV4` |        `BySummoner(Encrypted_SummonerId)`         |       `Summoner`        |
| `AccountV1`  |      `AccountsByPuuid(puuid)`      |       `Account`        |
| `AccountV1`  |        `AccountsByRiotId(gameName, riotId)`      |       `Account`        |
| `AccountV1`  | `ActiveShardsByGameByPuuid(EGame, puuid)` |       `ShardAccount`        |

<br>

> If you need more endpoints/methods you can open a issue or (i prefer) `Collaborate by pull request`.

### Rate limiter

Currently rate limiter is built on `RiotSingletonRequestLimiter` class

`RiotSingletonRequestLimiter` is singleton so configuration change will occur on all usages.

Limit per second / per 2 minutes is defined by `Api` object contructor.

```cs
Api api = new Api(<RIOT APP KEY>, <OneSecLimit>, <TwoMinLimit>, ELocation.EUNE);
```

Current implementation is very simple. `RiotRequestSender` will wait some time if 1 sec/2 min limit is exceeded.

Also Riot api is using `Retry-After` header if `Method-Rate-Limit` is exceeded, limiter will try to retry request **three times** when `any error` status code occurred after delay presented in `Retry-After` header.

You can change default retry count by modify constructor of `RiotRequestSenderBuilder` with specified parameter:

```cs
new RiotRequestSenderBuilder(<optional retry count>);
```

---

### **Simple way of sending requests**

Instead of getting fun with bunch of builder classes you can just use **FacadeClass** created to simplify this process.

To send request you can just use

```cs
RiotFacade facade = new RiotFacade(ELocation.EUNE);
// RiotFacade facade = new RiotFacade(); - default location from FacadeGlobalConfig
// facade.SetLocation(ELocation.EUW); - to change request location
RiotResponse response = await facade.GetAsync(facade.ApiInstance.SummonerV4.ByName("Rekurencja"));
```

thats all, to configure this approach you should configure `FacadeGlobalConfig` file

| Configuration     |                     Description                      | Configuration |
| ----------------- | :--------------------------------------------------: | :-----------: |
| `Api_key`         |                   Set riot api key                   |  `Mandatory`  |
| `Elocation`       |    Default location of requests - default `EUNE`     |  `Mandatory`  |
| `OneSecondLimit`  |   Set maximum request rate per second - default 10   |   optional    |
| `TwoMinutesLimit` | Set maximum request rate per 2 minutes - default 100 |   optional    |
| `MaxRetryCount`   |  Set amount of retry if request failed - default 3   |   optional    |

---

## Tests

All classes are tested by `Test.Integration` and `Test.Unit` projects.
You can always run it .

`Test.Integration` project needs the small configuration - just replace `API_KEY` field in `IntegrationConfiguration.cs` file.

If you dont have a production key you can always use development.

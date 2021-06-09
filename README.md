# LeagueTrack
C# Based platform for tracking players.

✔️ - Done
⚙️ - WIP
❌ - TODO
| Feature        | Status       |
| ------------- |:-------------:| 
| Riot API Wrapper     | ✔️ | 
| Riot API Wrapper rate limiter     | ⚙️ | 
| Rest API     | ❌ |
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


All parts of this project are allowed to re-use for your own - including modifications, distribution, private and commercial use.

`Collaborate.`


---
## API Example

To use api you have to create new Api instance
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
director.builder = new RiotRequestSenderBuilder();
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
| Endpoint        | Method       | Response Mapping Object   |
| ------------- |:-------------:|:-------------:| 
| `SummonerV4`     | `ByName(Summoner_Name)` | `Summoner` |
| `SummonerV4`     | `ByAccount(Encrypted_account_id)` | `Summoner` | 
| `SummonerV4`     | `ByPuuid(Encrypted_Puuid)` | `Summoner` |
| `SummonerV4` | `BySummoner(Encrypted_SummonerId)` | `Summoner` |

<br>

>New endpoints will be created only if this project need a new one.  
>If you need more endpoints/methods you can open a issue or (i prefer) `Collaborate by pull request`


**Rate limiter is still in `Work in progress` state.**

---

## Tests
All classes are tested by `Test.Integration` and `Test.Unit` projects.
You can always run it .

`Test.Integration` project needs the small configuration - just replace `API_KEY` field in `IntegrationConfiguration.cs` file. 

If you dont have a production key you can always use development.

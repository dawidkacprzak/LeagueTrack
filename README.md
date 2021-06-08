# LeagueTrack
C# Based platform for tracking players.

✔️ - Done
⚙️ - WIP
❌ - TODO
| Feature        | Status       |
| ------------- |:-------------:| 
| Riot API Wrapper     | ⚙️ | 
| Riot API Wrapper rate limiter     | ❌ | 
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
Api instance = Api.GetInstance(<RIOT APP KEY>, <OneSecLimit>, <TwoMinLimit>, ELocation.EUNE);
```
Just insert your `api key` from RIOT Developer portal https://developer.riotgames.com/

After registration your project is restricted by limited `api rate limit`. 

From project settings (look at riot developer portal -> app register) check your rate limits and put corresponding values to `OneSecLimit` and `TwoMinLimit` fields. 

`ELocation` enum stands for game server.

<br>

Create request object with following statement

```cs
RiotRequest request = instance.SummonerV4.ByName("Rekurencja");
```
`SummonerV4` stands for **endpoint** and **ByName** is just method.

`RiotRequest` object is wrapped by your api key in headers. You can check it in `HeaderParameters` field (`GetHeaderParams method`).

You can also check final HTTP link from `HttpAddress` field.    
<br>
Full list of **endpoints** and **methods** is in `Work in progress` state 

Rate limiter and request invoker classes are still in `Work in progress` state.

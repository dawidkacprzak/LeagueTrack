# LeagueTrack
C# Based platform for tracking players.

✔️ - Done
⚙️ - WIP
❌ - TODO
| Feature        | Status       |
| ------------- |:-------------:| 
| Riot API Wrapper     | ⚙️ | 
| Rest API     | ❌ |
| Rest API Rate limiter | ❌ |
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
- Easy op.gg, professor.gg check
- Analyze each game progress like builds, runes, items etc.


All parts of this project are allowed to re-use for your own - including modifications, distribution, private and commercial use.

`Collaborate.`


---
## API Example

To use api you have to create new Api instance
```cs
Api instance = Api.GetInstance(<RIOT APP KEY>, <OneMinLimit>, <TwoMinLimit>, ELocation.EUNE);
```
Just insert your api key from RIOT Developer portal https://developer.riotgames.com/

After registration your project is restricted by limited api rate limit. 

Just fill OneMinLimit and TwoMinLimit fields with your needs. 

After that just fill ELocation enum which stands for game server.

You can create request object with following statement

```cs
RiotRequest request = instance.SummonerV4.ByName("Rekurencja");
```
SummonerV4 stands for endpoint and ByName is just method.

`RiotRequest` object is wrapped by your api key in headers. You can check it in `HeaderParameters` field (`GetHeaderParams method`).

You can also check final HTTP link from `HttpAddress` field.

Rate limiter and request invoker classes are still in `Work in progress` state.

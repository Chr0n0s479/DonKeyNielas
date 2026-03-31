import { MatchForecastEnum } from "../enums/MatchForecast.enum"
import { ITeam } from "./ITeam.model"

export interface IMatchForecast{
    matchId: number
    homeTeam: ITeam
    visitTeam: ITeam
    forecast: MatchForecastEnum | null
}
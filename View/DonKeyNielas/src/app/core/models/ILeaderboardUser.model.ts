import { ILeaderboardForecast } from "./ILeaderboardForecast.model"

export interface ILeaderboardUser {
    userId: number
    userName: string
    hits:number

    forecasts: ILeaderboardForecast[]
}


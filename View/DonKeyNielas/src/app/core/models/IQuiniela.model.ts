import { IMatchForecast } from "./IMatchForecast.model";

export interface IQuiniela {
    championshipId: number,
    matchWeek: number,
    forecasts: IMatchForecast[]

}
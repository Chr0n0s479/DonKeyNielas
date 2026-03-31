import { IMatchForecast } from "./IMatchForecast.model";
import { IMatchIdForecast } from "./IMatchIdForecast.model";

export interface IQuinielaCreate {
    championshipId: number,
    matchWeek: number,
    forecasts: IMatchIdForecast[]

}
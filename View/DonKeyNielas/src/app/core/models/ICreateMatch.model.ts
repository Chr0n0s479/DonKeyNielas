import { IMatchForm } from "./IMatchForm.model"

export interface ICreateMatch {
    championshipId: number
    matchWeek: number
    matches: IMatchForm[]

}
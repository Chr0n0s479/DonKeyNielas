import { IMatchScoreId } from "./IMatchScoreId.modal"

export interface IMatchGroup{
    championshipId: number
    matchWeek: number
    matches: IMatchScoreId[]
}
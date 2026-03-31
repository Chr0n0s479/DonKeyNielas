import { ITeam } from "./ITeam.model";

export interface ICompleteMatch {
  id: number;
  matchDate: Date;
  homeTeam: ITeam;
  visitTeam: ITeam;
  scoreHomeTeam?: number;
  scoreVisitTeam?: number;
}
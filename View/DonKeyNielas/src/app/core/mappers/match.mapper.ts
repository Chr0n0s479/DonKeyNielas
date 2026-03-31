import { ICompleteMatch } from "../models/ICompleteMatch.model";
import { IMatchCreateQuiniela } from "../models/IMatchCreateQuiniela.model";
import { IMatchDto } from "../models/IMatchDto.model";
import { IMatchForecast } from "../models/IMatchForecast.model";
import { IMatchScore } from "../models/IMatchScore.model";
import { IMatchScoreId } from "../models/IMatchScoreId.modal";

export class MatchMapper {

  static fromDto(dto: IMatchDto): ICompleteMatch {
    return {
      id: dto.id,
      matchDate: new Date(dto.matchDate),

      homeTeam: {
        id: dto.homeTeamId,
        name: dto.homeTeamName,
        imageFile: dto.homeTeamImage
      },

      visitTeam: {
        id: dto.visitTeamId,
        name: dto.visitTeamName,
        imageFile: dto.visitTeamImage
      },

      scoreHomeTeam: dto.scoreHomeTeam,
      scoreVisitTeam: dto.scoreVisitTeam
    };
  }

  static fromDtoList(dtos: IMatchDto[]): ICompleteMatch[] {
    return dtos.map(dto => this.fromDto(dto));
  }

  static fromCompleteToMatchIdDto(dto: ICompleteMatch): IMatchScoreId{
    return {
      matchId: dto.id,
      homeScore: dto.scoreHomeTeam!,
      visitScore: dto.scoreVisitTeam!
    }
  }

  static fromCompleteToMatchIdList(dtos: ICompleteMatch[]): IMatchScoreId[]{
    return dtos.map(dto => this.fromCompleteToMatchIdDto(dto))
  }
  static fromCompleteToMatchForecast(dto: ICompleteMatch): IMatchForecast{
    return {
      matchId: dto.id,
      homeTeam: dto.homeTeam!,
      visitTeam: dto.visitTeam!,
      forecast: null
    }
  }

  static fromCompleteToMatchForecastList(dtos: ICompleteMatch[]): IMatchForecast[]{
    return dtos.map(dto => this.fromCompleteToMatchForecast(dto))
  }

}
export interface IMatchDto{
    id: number
    matchDate: Date
    homeTeamId: number
    homeTeamName: string
    homeTeamImage: string
    visitTeamId: number
    visitTeamName: string
    visitTeamImage: string
    scoreHomeTeam: number
    scoreVisitTeam: number
}

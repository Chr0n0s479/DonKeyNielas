export interface ILeaderboardMatch{
    matchId: number

    homeTeamName: string
    homeImage:string
    scoreHome: number

    visitTeamName: string
    visitImage: string
    scoreVisit: number
    result: string
}
import { ILeaderboardMatch } from "./ILeaderboardMatch.model";
import { ILeaderboardUser } from "./ILeaderboardUser.model";

export interface ILeaderboard {
    matches: ILeaderboardMatch[]
    users: ILeaderboardUser[]
}
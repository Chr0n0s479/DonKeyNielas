import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ICompleteMatch } from '../../../models/ICompleteMatch.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IMatchScore } from '../../../models/IMatchScore.model';

@Component({
  selector: 'app-set-match-results',
  imports: [CommonModule, FormsModule],
  templateUrl: './set-match-results.html',
  styleUrl: './set-match-results.css',
})
export class SetMatchResults {
  @Input() match: ICompleteMatch | null = null;
  @Output() scoreChanged = new EventEmitter<IMatchScore>();

  onScoreChanged(){
    if(!this.match)
      return
    this.scoreChanged.emit({
      homeScore: this.match!.scoreHomeTeam!,
      visitScore: this.match!.scoreVisitTeam!
    })

  }
}

import { Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { Auth } from '../../core/services/auth';

@Component({
  selector: 'app-panel',
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './panel.html',
  styleUrl: './panel.css',
})
export class Panel {

  constructor(private router: Router,
    public authService: Auth
  ) {}

  logout() {

    localStorage.removeItem("token");

    this.router.navigate(['/login']);

  }

}
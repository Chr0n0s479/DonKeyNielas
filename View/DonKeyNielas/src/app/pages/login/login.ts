import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Auth } from '../../core/services/auth';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-login',
  imports: [RouterLink, FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  email = "";
  password = "";

  constructor(
    private authService: Auth,
    private router: Router
  ) { }

  login() {
    Swal.fire({
      title: 'Iniciando sesión...',
      allowOutsideClick: false,
      didOpen: () => {
        Swal.showLoading();
      }
    });
    this.authService.login({
      email: this.email,
      password: this.password
    })
      .subscribe({

        next: res => {
          Swal.close();
          Swal.fire({
            icon: 'success',
            title: 'Bienvenido',
            text: 'Login exitoso',
            timer: 1500,
            showConfirmButton: false
          });
          localStorage.setItem("token", res.token);
          this.router.navigate(['/home']);

        },

        error: err => {
          Swal.close();
          Swal.fire({
            icon: 'error',
            title: 'Error al iniciar sesión',
            text: `${err.error}`,
            timer: 1500,
            showConfirmButton: false
          });
          console.error(err);

        }

      });

  }
}

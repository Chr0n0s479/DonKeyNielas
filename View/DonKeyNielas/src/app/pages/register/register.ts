import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Auth } from '../../core/services/auth';
import { RegisterRequest } from '../../core/models/Iregister-request.model';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  username = '';
  email = '';
  fullname = '';
  password = '';
  inviteCode = '';


  constructor(
    private authService: Auth,
    private router: Router
  ) { }


  register() {
    const request: RegisterRequest = {
      username: this.username,
      email: this.email,
      fullname: this.fullname,
      password: this.password,
      inviteCode: this.inviteCode
    }
    Swal.fire({
      title: 'Procesando solicitud...',
      allowOutsideClick: false,
      didOpen: () => {
        Swal.showLoading();
      }
    });

    this.authService.register(request)
      .subscribe({
        next: res => {
          Swal.close();
          Swal.fire({
            icon: 'success',
            title: 'Registro Correcto',
            text: 'Será redireccionado al Login',
            timer: 1500,
            showConfirmButton: false
          });
          this.router.navigate(['/login']);
          console.log(res)
        },
        error: err => {
          Swal.close();
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: `${err.error}`,
            timer: 1500,
            showConfirmButton: false
          });
          console.log("Error al registrar")
          console.log(err)
        }
      })
  }

}

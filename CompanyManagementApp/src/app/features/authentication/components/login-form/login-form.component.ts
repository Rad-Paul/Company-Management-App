import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { AuthenticationService } from '@services/authentication/authentication.service';
import UserForLogin from '@shared/models/users/userForLogin';
import LoginResponse from '@shared/models/users/loginResponse';


@Component({
  selector: 'app-login-form',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, ReactiveFormsModule],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.css'
})
export class LoginFormComponent {
  #authService : AuthenticationService = inject(AuthenticationService);
  #router : Router = inject(Router);

  loginForm : FormGroup = new FormGroup ({
    username: new FormControl(""),
    password: new FormControl("")
  });

  onFormSubmit(){
    console.log(this.loginForm.value);
    this.#authService.login(this.loginForm.value as UserForLogin)
      .subscribe({
        next: (response) => {
          const loginResponse : LoginResponse = response as LoginResponse;
          console.log(response)
          localStorage.setItem('token', loginResponse.token)
          this.#router.navigate(['companies']);
        },
        error: (err) => {
          console.log(err)
        }
    })
  }
}

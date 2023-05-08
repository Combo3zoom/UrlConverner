import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import {Subject, takeUntil} from "rxjs";
import {passwordValidator} from "./password.validator";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      name: ['', [Validators.required]],
      password: ['', [Validators.required, passwordValidator]],
    });
  }

  private destroy$ = new Subject<void>();
  ngOnInit(): void {}

  onSubmit() {
    if (this.loginForm.valid) {
      const { name, password } = this.loginForm.value;
      this.http.post('https://localhost:7009/api/auth/login', { name, password }, { withCredentials: true })
        .pipe(takeUntil(this.destroy$))
        .subscribe((response: any) => {
          localStorage.setItem('userRole', response.role);
          localStorage.setItem('userName', name);

          this.router.navigate(['/short-urls']);
        }, (error) => {
        });
    }
  }
}

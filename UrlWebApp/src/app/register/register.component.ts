import {Subject, takeUntil} from "rxjs";
import {Component, OnDestroy, OnInit} from "@angular/core";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {passwordValidator} from "../login/password.validator";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit, OnDestroy {
  registerForm!: FormGroup;
  private destroy$ = new Subject<void>();

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.initRegisterForm();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  initRegisterForm(): void {
    this.registerForm = this.fb.group({
      name: ['', [Validators.required]],
      password: [
        '',
        [
          Validators.required,
          passwordValidator,
        ],
      ],
    });
  }

  onSubmit(): void {
    if (this.registerForm!.valid) {
      const { name, password } = this.registerForm!.value;
      this.http
        .post('https://localhost:7009/api/Auth/Register', { name, password })
        .pipe(takeUntil(this.destroy$))
        .subscribe(() => {
          this.router.navigate(['/login']);
        });
    }
  }
}

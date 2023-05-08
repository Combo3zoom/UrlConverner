import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export const passwordValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
  const value = control.value;
  const hasUpperCase = /[A-Z]/.test(value);
  const hasSpecialSymbol = /[!@#$%^&*(),.?":{}|<>]/.test(value);
  const hasDigit = /\d/.test(value);

  if (!hasUpperCase || !hasSpecialSymbol || !hasDigit) {
    return { passwordRequirements: true };
  }

  return null;
};

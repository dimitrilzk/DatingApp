import { Component, inject, OnInit, output } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { TextInputComponent } from '../_forms/text-input/text-input.component';
import { DatePickerComponent } from '../_forms/date-picker/date-picker.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, TextInputComponent, DatePickerComponent], //ep 136
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements OnInit {
  //passare dati da componente padre (home) a figlio (register) l.55 (vecchio modo)
  // @Input() usersFromHomeComponent: any;
  //il nuovo modo per passare dati da padre a figlio è questo: disponibile da verisone 17.3 di angular
  // usersFromHomeComponent = input.required<any>();

  //passare dati dal comoponente figlio al padre l.56 (vecchio modo)
  // @Output() cancelRegister = new EventEmitter();
  //il nuovo modo per passare dati dal figlio al padre ps non serve levent emitter
  cancelRegister = output<boolean>();

  private accountService = inject(AccountService);
  private fb = inject(FormBuilder);
  private router = inject(Router);
  registerForm: FormGroup = new FormGroup({});
  maxDate = new Date();
  validationErrors: string[] | undefined;

  ngOnInit(): void {
    this.initializeForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }

  initializeForm() {
    //switch to form builder 141
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      password: [
        '',
        [Validators.required, Validators.minLength(4), Validators.maxLength(8)],
      ],
      confirmPassword: [
        '',
        [Validators.required, this.matchValues('password')],
      ],
      gender: ['male'],
      knownAs: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
    });
    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () =>
        this.registerForm.controls['confirmPassword'].updateValueAndValidity(),
    });
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value
        ? null
        : { isMatching: true };
    };
  }

  register() {
    const dob = this.getDateOnly(this.registerForm.get('dateOfBirth')?.value);
    //patchValue 145 5
    this.registerForm.patchValue({ dateOfBirth: dob });

    this.accountService.register(this.registerForm.value).subscribe({
      next: (_) => this.router.navigateByUrl('/members'),
      error: (error) => (this.validationErrors = error),
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

  private getDateOnly(dob: string | undefined) {
    if (!dob) {
      return;
    }

    return new Date(dob).toISOString().slice(0, 10);
  }
}

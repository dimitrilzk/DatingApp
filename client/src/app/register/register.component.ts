import { Component, inject, OnInit, output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule], //ep 136
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
  private toastr = inject(ToastrService);
  model: any = {};
  registerForm: FormGroup = new FormGroup({});

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.registerForm = new FormGroup({
      username: new FormControl(),
      password: new FormControl(),
      confirmPassword: new FormControl(),
    });
  }

  register() {
    console.log(this.registerForm.value);
    // this.accountService.register(this.model).subscribe({
    //   next: (response) => {
    //     console.log(response);
    //     this.cancel();
    //   },
    //   error: (error) => this.toastr.error(error.error, 'Registration Error'),
    // });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}

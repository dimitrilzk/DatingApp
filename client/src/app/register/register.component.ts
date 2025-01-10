import { Component, inject, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  //passare dati da componente padre (home) a figlio (register) l.55 (vecchio modo)
  // @Input() usersFromHomeComponent: any;
  //il nuovo modo per passare dati da padre a figlio è questo: disponibile da verisone 17.3 di angular
  // usersFromHomeComponent = input.required<any>();

  //passare dati dal comoponente figlio al padre l.56 (vecchio modo)
  // @Output() cancelRegister = new EventEmitter();
  //il nuovo modo per passare dati dal figlio al padre ps non serve levent emitter
  cancelRegister = output<boolean>();

  private accountService = inject(AccountService);
  model: any = {};

  register() {
    this.accountService.register(this.model).subscribe({
      next: response => {
        console.log(response);
        this.cancel();
      },
      error: error => console.log(error)
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}

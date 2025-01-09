import { Component, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';

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
  usersFromHomeComponent = input.required<any>();

  //passare dati dal comoponente figlio al padre l.56 (vecchio modo)
  // @Output() cancelRegister = new EventEmitter();
  //il nuovo modo per passare dati dal figlio al padre ps non serve levent emitter
  cancelRegister = output<boolean>();

  model: any = {};

  register() {
    console.log(this.model);
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}

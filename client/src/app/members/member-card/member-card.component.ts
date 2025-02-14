import { Component, input } from '@angular/core';
import { Member } from '../../_models/member';

@Component({
  selector: 'app-member-card',
  standalone: true,
  imports: [],
  templateUrl: './member-card.component.html',
  styleUrl: './member-card.component.css'
})
export class MemberCardComponent {
  //questo è il modo nuovo di riceve parametri in input da comp padri 
  //in realtà l'input qui sotto è un extension method del signal 
  //quindi nel template bisogna chimarlo es: member()
  member = input.required<Member>();
}

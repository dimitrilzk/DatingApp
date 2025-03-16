import { Component, input } from '@angular/core';
import { Member } from '../../_models/member';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-member-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './member-card.component.html',
  styleUrl: './member-card.component.css',
  // encapsulation: ViewEncapsulation.None
  //encapsulation none toglie il tag univoco nell'html dell'elemento generato e quindi il css di questo comp diventa globale
})
export class MemberCardComponent {
  //questo è il modo nuovo di riceve parametri in input da comp padri
  //in realtà l'input qui sotto è un extension method del signal
  //quindi nel template bisogna chimarlo es: member()
  member = input.required<Member>(); //todo
}

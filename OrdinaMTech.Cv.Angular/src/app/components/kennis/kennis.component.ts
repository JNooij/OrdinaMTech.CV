import { Component, Input } from '@angular/core';
import { Ervaring } from '../../../models/ervaring.model';
import { Kennis } from '../../../models/kennis.model';

enum KennisNiveau {
  Geen = 0,
  Basiskennis = 1,
  Gemiddeld = 2,
  Ervaren = 3,
  Zeer_Ervaren = 4,
  Expert = 5
}

@Component({
  selector: 'app-kennis',
  templateUrl: './kennis.component.html',
  styleUrl: './kennis.component.css'
})
export class KennisComponent {
  @Input()
  kennis: Kennis[] = []

  getNiveauTekst(niveau: number): string {
    return KennisNiveau[niveau] || 'Onbekend';
  }
}

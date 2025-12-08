import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

export interface FiltriListe {
  orderBy: string;
  ascending: boolean;
  creataDa: string | null;
  conclusa: boolean | null;
}

@Component({
  selector: 'app-liste-filters',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './liste-filters.html',
  styleUrls: ['./liste-filters.css']
})
export class ListeFiltersComponent {
  
  @Output() filtriChange = new EventEmitter<FiltriListe>();

  // Filtri correnti
  orderBy: string = 'DataCreazione';
  ascending: boolean = false;
  creataDa: string = '';
  conclusa: string = 'tutte'; 

  applyFilters(): void {
    const filtri: FiltriListe = {
      orderBy: this.orderBy,
      ascending: this.ascending,
      creataDa: this.creataDa.trim() || null,
      conclusa: this.conclusa === 'tutte' ? null : this.conclusa === 'concluse'
    };

    console.log('🎯 Filtri da applicare:', filtri);
    this.filtriChange.emit(filtri);
  }

  resetFilters(): void {
    this.orderBy = 'DataCreazione';
    this.ascending = false;
    this.creataDa = '';
    this.conclusa = 'tutte';
    this.applyFilters();
  }
}

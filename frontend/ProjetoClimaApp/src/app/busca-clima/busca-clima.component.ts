import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { ClimaService } from '../clima.service';
import { StateService } from '../state.service';

@Component({
  selector: 'app-busca-clima',
  imports: [FormsModule],
  templateUrl: './busca-clima.component.html',
  styleUrl: './busca-clima.component.css',
  standalone: true,
})
export class BuscaClimaComponent {
  cidade: string = '';
  dadosClima: any;

  constructor(
    private climaService: ClimaService,
    private stateService: StateService
  ) {}

  buscarClima() {
    this.climaService.getClima(this.cidade).subscribe((data) => {
      this.dadosClima = data;
      this.stateService.setCidadeAtual(this.cidade);
    });
  }
}

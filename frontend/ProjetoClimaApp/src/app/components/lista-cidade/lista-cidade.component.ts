import { Component, OnInit } from '@angular/core';
import { DadosClima } from '../../models/DadosClima.model';
import { CidadeFavorita } from '../../models/CidadeFavorita';
import { ClimaService } from '../../services/clima.service';
import { CidadeFavoritaService } from '../../services/cidade-favorita.service';
import { CidadeStateService } from '../../services/cidade-state.service';
import { ClimaCardComponent } from '../clima-card/clima-card.component';

@Component({
  selector: 'app-lista-cidade',
  imports: [ClimaCardComponent],
  templateUrl: './lista-cidade.component.html',
  styleUrl: './lista-cidade.component.css',
  standalone: true,
})
export class ListaCidadeComponent implements OnInit {
  buscaCidade: string = '';
  dadosClima: DadosClima | null = null;
  favoritos: CidadeFavorita[] = [];

  constructor(
    private climaService: ClimaService,
    private cidadeFavoritaService: CidadeFavoritaService,
    private cidadeStateService: CidadeStateService
  ) {}

  ngOnInit() {
    this.cidadeStateService.favorites$.subscribe((favorites) => {
      this.favoritos = favorites;
    });
    this.cidadeFavoritaService.obterFavoritos();
  }

  async buscaClima() {
    if (this.buscaCidade) {
      this.dadosClima = await this.climaService.obterClima(this.buscaCidade);
    }
  }

  addFavorite(cidade: string) {
    this.cidadeFavoritaService.adicionaFavorito(cidade);
  }

  removeFavorite(cidade: string) {
    this.cidadeFavoritaService.removerFavorito(cidade);
  }
}

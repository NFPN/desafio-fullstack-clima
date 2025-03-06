export interface DadosClima {
  name: string;
  main: DadosPrincipais;
  weather: Clima[];
}

export interface Clima {
  description: string;
  icon: string;
}

export interface DadosPrincipais {
  temp: number;
  feelsLike: number;
  humidity: number;
}

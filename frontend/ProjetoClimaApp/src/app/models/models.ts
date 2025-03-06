export interface LoginModel {
  email: string;
  password: string;
}

export interface RegistroModel {
  email: string;
  password: string;
}

export interface DadosClima {
  name: string;
  main: DadosPrincipais;
  weather: Clima[];
}

export interface DadosPrincipais {
  temp: number;
  feelsLike: number;
  humidity: number;
}

export interface Clima {
  description: string;
  icon: string;
}

export interface DadosPrevisao {
  list: PrevisaoItem[];
  city: Cidade;
}

export interface PrevisaoItem {
  dt: number;
  main: DadosPrincipais;
  weather: Clima[];
}

export interface Cidade {
  name: string;
}

export interface CidadeFavorita {
  id: number;
  nomeCidade: string;
  idUsuario: string;
}

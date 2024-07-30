using AutoMapper;
using EstudoMinimalAPI.Models;
using EstudoMinimalAPI.Entities;


namespace EstudoMinimalAPI.Profiles; 
public class RangoMinimalProfile : Profile {

    public RangoMinimalProfile()
    {
        CreateMap<Rango, RangoDTO>().ReverseMap();
        CreateMap<Rango, RangoParaCriacaoDTO>().ReverseMap();
        CreateMap<Rango, RangoParaAtualizacaoDTO>().ReverseMap();
        CreateMap<Rango, RangoParaDelecaoDTO>().ReverseMap();
        CreateMap<Ingrediente, IngredienteDTO>()
            .ForMember(
                d => d.RangoId,
                o => o.MapFrom(s => s.Rangos.First().Id)
        );

        //Linha 1: CreateMap<Ingrediente, IngredienteDTO>()
        //CreateMap<Ingrediente, IngredienteDTO>(): Este método cria um mapeamento entre duas classes,
        //Ingrediente e IngredienteDTO. Isso diz ao AutoMapper que você quer configurar como os objetos de
        //Ingrediente serão convertidos em objetos de IngredienteDTO.

        //Linha 2: .ForMember(
        //.ForMember(: Este método é usado para especificar configurações de mapeamento para um membro específico
        //da classe de destino(IngredienteDTO).

        //Linha 3: d => d.RangoId,
        //d => d.RangoId,: Isso especifica o membro de destino que estamos configurando.d é um parâmetro representando
        //a instância de IngredienteDTO, e RangoId é a propriedade de IngredienteDTO que queremos configurar.

        //Linha 4: o => o.MapFrom(s => s.Rangos.First().Id)
        //o => o.MapFrom(s => s.Rangos.First().Id): Isso especifica a fonte do valor que será mapeado para RangoId
        //em IngredienteDTO.o representa a configuração para o membro de destino.MapFrom define como o valor será
        //obtido da instância de origem.s é um parâmetro representando a instância de Ingrediente, e s.Rangos.First().Id
        //indica que queremos mapear o valor da propriedade Id do primeiro elemento da coleção Rangos em Ingrediente.
        //Explicação Completa

        //O código está mapeando a propriedade RangoId do IngredienteDTO a partir do Id do primeiro Rango na coleção
        //Rangos do Ingrediente.Ou seja, quando o AutoMapper criar um IngredienteDTO a partir de um Ingrediente, ele
        //vai pegar o Id do primeiro elemento na coleção Rangos do Ingrediente e atribuir esse valor à propriedade
        //RangoId do IngredienteDTO.
        //
        
    
    }

}

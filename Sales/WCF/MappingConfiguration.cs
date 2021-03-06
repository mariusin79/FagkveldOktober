﻿using AutoMapper;
using Sales.Automapping;
using Sales.Contracts;
using SharedContracts;
using Mapper = AutoMapper.Mapper;

namespace Sales.WCF
{
    public class MappingConfiguration : IMappingConfiguration
    {
        public void Setup()
        {
            Mapper.CreateMap<int, BookKey>().ConstructUsing(src => new BookKey {Value = src});
            Mapper.CreateMap<long, Money>().ConstructUsing(src => new Money {PriceInOere = src});

            Mapper.CreateMap<ReadModel.RmAvailableBook, AvailableBook>().ForMember(dst => dst.Id, opt => opt.ResolveUsing(src => src.BookKey));
        }
    }
}
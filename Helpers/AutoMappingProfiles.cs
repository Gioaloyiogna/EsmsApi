﻿using AutoMapper;
using ServiceManagerApi.Data;
using ServiceManagerApi.Dtos.Compartments;
using ServiceManagerApi.Dtos.Groups;
using ServiceManagerApi.Dtos.HoursEntries;
using ServiceManagerApi.Dtos.Items;
using ServiceManagerApi.Dtos.ItemValue;
using ServiceManagerApi.Dtos.LubeBrands;
using ServiceManagerApi.Dtos.LubeConfigs;
using ServiceManagerApi.Dtos.LubeEntry;
using ServiceManagerApi.Dtos.LubeGrades;
using ServiceManagerApi.Dtos.ProductionActivity;
using ServiceManagerApi.Dtos.ProductionMineArea;
using ServiceManagerApi.Dtos.ProductionShift;
using ServiceManagerApi.Dtos.RefillTypes;
using ServiceManagerApi.Dtos.Resolution;
using ServiceManagerApi.Dtos.ResolutionTypes;
using ServiceManagerApi.Dtos.Sections;
using ServiceManagerApi.Dtos.Services;

namespace ServiceManagerApi.Helpers
{
    public class AutoMappingProfiles :Profile
    {
        public AutoMappingProfiles()
        {
            CreateMap<RefillTypePostDto, RefillType>();
            CreateMap<ItemsPostDto, Item>();          
            CreateMap<ItemValuePostDto, ItemValue>();          
            CreateMap<GroupsPostDto, Group>();          
            CreateMap<SectionPostDto, Section>();          
            CreateMap<ServicePostDto, Service>();     
            CreateMap<LubeBrandPostDto, LubeBrand>();
            CreateMap<LubeGradePostDto, LubeGrade>();
            CreateMap<LubeConfigPostDto, LubeConfig>();
            CreateMap<LubeEntryPostDto, LubeEntry>();
            CreateMap<CompartmentPostDto, Compartment>();
            CreateMap<ResolutionPostDto, Resolution>();
            CreateMap<HoursEntriesPostDto, HoursEntry>();
            CreateMap<ResolutionTypePostDto, ResolutionType>();
            CreateMap<ProductionActivityPostDto, ProductionActivity>();
            CreateMap<ProductionMineAreaPostDto, ProductionMineArea>();
            CreateMap<ProductionShiftPostDto, ProductionShift>();
        }
    }
}
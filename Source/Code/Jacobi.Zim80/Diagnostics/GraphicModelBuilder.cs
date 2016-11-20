using Jacobi.Zim80.Components;
using Jacobi.Zim80.Components.CpuZ80;
using Jacobi.Zim80.Components.Memory;
using Jacobi.Zim80.Diagnostics.DgmlModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Jacobi.Zim80.Diagnostics
{
    public sealed class GraphicModelBuilder
    {
        private readonly UniqueNameBuilder _idBuilder = new UniqueNameBuilder();
        private readonly DgmlModelBuilder _dgmlBuilder = new DgmlModelBuilder("Zim80 Model");
        private readonly List<object> _visited = new List<object>();

        public void Save(Stream fileStream)
        {
            DgmlSerializer.Serialize(fileStream, _dgmlBuilder.DirectedGraph);
        }

        public bool DisplayComponents { get; set; }
        public bool DisplayUnconnected { get; set; }

        public void Add(DigitalSignal digitalSignal, 
            bool inclProviders = true, bool inclConsumers = true)
        {
            if (!Visit(digitalSignal)) return;

            var node = AddNode<DigitalSignal>(digitalSignal.Name);

            if (inclProviders)
                foreach(var p in digitalSignal.Providers)
                    Add(p);

            if (inclConsumers)
                foreach (var c in digitalSignal.Consumers)
                    Add(c);
        }

        public DirectedGraphNode Add(DigitalSignalProvider provider)
        {
            if (!Visit(provider) ||
                (!provider.IsConnected && !DisplayUnconnected))
                return Find<DigitalSignalProvider>(provider.Name);


            var node = AddNode<DigitalSignalProvider>(provider.Name);

            if (provider.IsConnected)
            {
                Add(provider.DigitalSignal);

                var dsid = _idBuilder.NewId<DigitalSignal>(provider.DigitalSignal.Name);
                _dgmlBuilder.AddLink(node.Id, dsid);
            }

            return node;
        }

        public DirectedGraphNode Add(DigitalSignalConsumer consumer)
        {
            if (!Visit(consumer) ||
                (!consumer.IsConnected && !DisplayUnconnected))
                return Find<DigitalSignalConsumer>(consumer.Name);

            var node = AddNode<DigitalSignalConsumer>(consumer.Name);

            if (consumer.IsConnected)
            {
                Add(consumer.DigitalSignal);

                var dsid = _idBuilder.NewId<DigitalSignal>(consumer.DigitalSignal.Name);
                _dgmlBuilder.AddLink(node.Id, dsid);
            }

            return node;
        }

        public void Add<T>(Bus<T> bus,
            bool inclMasters = true, bool inclSlaves = true)
            where T : BusData, new()
        {
            if (!Visit(bus)) return;

            var node = AddNode<Bus<T>>(bus.Name);

            if (inclMasters)
                foreach (var m in bus.Masters)
                    Add(m);

            if (inclSlaves)
                foreach (var s in bus.Slaves)
                    Add(s);
        }

        public DirectedGraphNode Add<T>(BusMaster<T> master)
            where T : BusData, new()
        {
            if (!Visit(master) ||
                (!master.IsConnected && !DisplayUnconnected))
                return Find<BusMaster<T>>(master.Name);

            var node = AddNode<BusMaster<T>>(master.Name);

            if (master.IsConnected)
            {
                Add(master.Bus);

                var bid = _idBuilder.NewId<Bus<T>>(master.Bus.Name);
                _dgmlBuilder.AddLink(node.Id, bid);
            }

            return node;
        }

        public DirectedGraphNode Add<T>(BusSlave<T> slave)
            where T : BusData, new()
        {
            if (!Visit(slave) ||
                (!slave.IsConnected && !DisplayUnconnected))
                return Find<BusSlave<T>>(slave.Name);

            var node = AddNode<BusSlave<T>>(slave.Name);

            if (slave.IsConnected)
            {
                Add(slave.Bus);

                var bid = _idBuilder.NewId<Bus<T>>(slave.Bus.Name);
                _dgmlBuilder.AddLink(node.Id, bid);
            }

            return node;
        }

        public DirectedGraphNode Add<T>(BusMasterSlave<T> masterSlave)
            where T : BusData, new()
        {
            if (!Visit(masterSlave) ||
                (!masterSlave.IsConnected && !masterSlave.Slave.IsConnected && !DisplayUnconnected))
                return Find<BusMasterSlave<T>>(masterSlave.Name);

            var node = Add((BusMaster<T>)masterSlave);
            Add(masterSlave.Slave);

            return node;
        }

        public DirectedGraphNode Add<AddressT, DataT>(MemoryRom<AddressT, DataT> rom)
            where AddressT : BusData, new()
            where DataT : BusData, new()
        {
            if (!Visit(rom))
                return Find<MemoryRom<AddressT, DataT>>();

            DirectedGraphNode node = null;

            if (DisplayComponents)
                node = AddNode<MemoryRom<AddressT, DataT>>();

            LinkContains(node, Add(rom.Address));
            LinkContains(node, Add(rom.Data));
            LinkContains(node, Add(rom.ChipEnable));
            LinkContains(node, Add(rom.OutputEnable));

            return node;
        }

        public DirectedGraphNode Add<AddressT, DataT>(MemoryRam<AddressT, DataT> ram)
            where AddressT : BusData, new()
            where DataT : BusData, new()
        {
            if (!Visit(ram))
                return Find<MemoryRam<AddressT, DataT>>();

            DirectedGraphNode node = null;

            if (DisplayComponents)
                node = AddNode<MemoryRam<AddressT, DataT>>();

            LinkContains(node, Add(ram.Address));
            LinkContains(node, Add(ram.Data));
            LinkContains(node, Add(ram.ChipEnable));
            LinkContains(node, Add(ram.OutputEnable));
            LinkContains(node, Add(ram.WriteEnable));

            return node;
        }

        public DirectedGraphNode Add(CpuZ80 cpu)
        {
            if (!Visit(cpu))
                return Find<CpuZ80>();

            DirectedGraphNode node = null;

            if (DisplayComponents)
                node = AddNode<CpuZ80>();

            LinkContains(node, Add(cpu.Address));
            LinkContains(node, Add(cpu.Data));
            LinkContains(node, Add(cpu.BusAcknowledge));
            LinkContains(node, Add(cpu.BusRequest));
            LinkContains(node, Add(cpu.Clock));
            LinkContains(node, Add(cpu.Halt));
            LinkContains(node, Add(cpu.Interrupt));
            LinkContains(node, Add(cpu.IoRequest));
            LinkContains(node, Add(cpu.MachineCycle1));
            LinkContains(node, Add(cpu.MemoryRequest));
            LinkContains(node, Add(cpu.NonMaskableInterrupt));
            LinkContains(node, Add(cpu.Read));
            LinkContains(node, Add(cpu.Refresh));
            LinkContains(node, Add(cpu.Reset));
            LinkContains(node, Add(cpu.Wait));
            LinkContains(node, Add(cpu.Write));

            return node;
        }

        private DirectedGraphNode AddNode<T>(string name = null)
        {
            var id = _idBuilder.NewId<T>(name);
            var node = _dgmlBuilder.AddNode(id);
            node.Label = id;
            node.TypeName = typeof(DigitalSignal).Name;
            node.Category1 = node.TypeName;

            return node;
        }

        private DirectedGraphLink LinkContains(DirectedGraphNode container, DirectedGraphNode containee)
        {
            var link = _dgmlBuilder.AddLink(container, containee);

            if (link != null)
            {
                link.Label = "Contains";
                link.Category1 = "Contains";
            }

            return link;
        }

        private DirectedGraphNode Find<T>(string name = null)
        {
            var id = _idBuilder.NewId<T>(name);

            return _dgmlBuilder.DirectedGraph.Nodes
                .SingleOrDefault(n => n.Id == id);
        }

        private bool Visit(object obj)
        {
            if (_visited.Contains(obj))
                return false;

            _visited.Add(obj);
            return true;
        }
    }
}

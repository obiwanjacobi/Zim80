using Jacobi.Zim80.Memory;
using Jacobi.Zim80.Diagnostics.DgmlModel;
using System.Collections.Generic;
using System.IO;
using System;

namespace Jacobi.Zim80.Diagnostics
{
    public sealed class GraphicModelBuilder
    {
        private readonly UniqueNameBuilder _idBuilder = new UniqueNameBuilder();
        private readonly DgmlModelBuilder _dgmlBuilder = new DgmlModelBuilder("Zim80 Model");
        private readonly Dictionary<object, DirectedGraphNode> _visited = new Dictionary<object, DirectedGraphNode>();

        public void Save(Stream fileStream)
        {
            DgmlSerializer.Serialize(fileStream, _dgmlBuilder.DirectedGraph);
        }

        public bool DisplayComponents { get; set; }
        public bool DisplayUnconnected { get; set; }
        public bool DisplayValues { get; set; }

        public void Add(DigitalSignal digitalSignal,
            bool inclProviders = true, bool inclConsumers = true)
        {
            if (!Visit(digitalSignal)) return;

            var node = AddNode<DigitalSignal>(digitalSignal.Name);
            SetNodeMap(digitalSignal, node);

            if (DisplayValues)
            {
                node.Description = digitalSignal.Level.ToString();
            }

            if (inclProviders)
                foreach(var p in digitalSignal.Providers)
                    Add(p);

            if (inclConsumers)
                foreach (var c in digitalSignal.Consumers)
                    Add(c);
        }

        public DirectedGraphNode Add(DigitalSignalProvider provider, string ownerName = null)
        {
            if (!Visit(provider) ||
                (!provider.IsConnected && !DisplayUnconnected))
                return FindNode(provider);

            var node = AddNode<DigitalSignalProvider>(provider.Name, ownerName, SafeNetName(provider));
            SetNodeMap(provider, node);

            if (DisplayValues)
            {
                node.Description = provider.Level.ToString();
            }

            if (provider.IsConnected)
            {
                Add(provider.DigitalSignal);

                var dsid = _idBuilder.NewId<DigitalSignal>(provider.DigitalSignal.Name);
                _dgmlBuilder.AddLink(node.Id, dsid);
            }

            return node;
        }

        public DirectedGraphNode Add(DigitalSignalConsumer consumer, string ownerName = null)
        {
            if (!Visit(consumer) ||
                (!consumer.IsConnected && !DisplayUnconnected))
                return FindNode(consumer);

            var node = AddNode<DigitalSignalConsumer>(consumer.Name, ownerName, SafeNetName(consumer));
            SetNodeMap(consumer, node);

            if (DisplayValues)
            {
                node.Description = consumer.Level.ToString();
            }

            if (consumer.IsConnected)
            {
                Add(consumer.DigitalSignal);

                var dsid = _idBuilder.NewId<DigitalSignal>(consumer.DigitalSignal.Name);
                _dgmlBuilder.AddLink(node.Id, dsid);
            }

            return node;
        }

        public void Add(Bus bus, string ownerName = null,
            bool inclMasters = true, bool inclSlaves = true)
        {
            if (!Visit(bus)) return;

            var node = AddNode<Bus>(bus.Name);
            SetNodeMap(bus, node);

            if (DisplayValues)
            {
                node.Description = bus.Value.ToString();
            }

            if (inclMasters)
                foreach (var m in bus.Masters)
                    Add(m);

            if (inclSlaves)
                foreach (var s in bus.Slaves)
                    Add(s);
        }

        public DirectedGraphNode Add(BusMaster master, string ownerName = null)
        {
            if (!Visit(master) ||
                (!master.IsConnected && !DisplayUnconnected))
                return FindNode(master);

            var node = AddInternal(master, ownerName, SafeNetName(master));

            return node;
        }

        private DirectedGraphNode AddInternal(BusMaster master, string ownerName, string netName)
        {
            var node = AddNode<BusMaster>(master.Name, ownerName, SafeNetName(master));
            SetNodeMap(master, node);

            if (DisplayValues)
            {
                node.Description = master.Value.ToString();
            }

            if (master.IsConnected)
            {
                Add(master.Bus);

                var bid = _idBuilder.NewId<Bus>(master.Bus.Name);
                _dgmlBuilder.AddLink(node.Id, bid);
            }

            return node;
        }

        public DirectedGraphNode Add(BusSlave slave, string ownerName = null)
        {
            if (!Visit(slave) ||
                (!slave.IsConnected && !DisplayUnconnected))
                return FindNode(slave);

            var node = AddNode<BusSlave>(slave.Name, ownerName, SafeNetName(slave));
            SetNodeMap(slave, node);

            if (DisplayValues)
            {
                node.Description = slave.Value.ToString();
            }

            if (slave.IsConnected)
            {
                Add(slave.Bus);

                var bid = _idBuilder.NewId<Bus>(slave.Bus.Name);
                _dgmlBuilder.AddLink(node.Id, bid);
            }

            return node;
        }

        public DirectedGraphNode Add(BusMasterSlave masterSlave, string ownerName = null)
        {
            if (!Visit(masterSlave) ||
                (!masterSlave.IsConnected && !masterSlave.Slave.IsConnected && !DisplayUnconnected))
                return FindNode(masterSlave);

            var node = AddInternal((BusMaster)masterSlave, ownerName, SafeNetName(masterSlave));
            SetNodeMap(masterSlave, node);

            if (DisplayValues)
            {
                node.Description = masterSlave.Value.ToString() + "/" + masterSlave.Slave.Value.ToString();
            }

            if (masterSlave.Slave.IsConnected)
            {
                var bid = _idBuilder.NewId<Bus>(masterSlave.Slave.Bus.Name);
                _dgmlBuilder.AddLink(node.Id, bid);
            }

            return node;
        }

        public DirectedGraphNode Add<AddressT, DataT>(MemoryRom<AddressT, DataT> rom)
            where AddressT : BusData, new()
            where DataT : BusData, new()
        {
            if (!Visit(rom))
                return FindNode(rom);

            DirectedGraphNode node = AddNode<MemoryRom<AddressT, DataT>>(rom.Name);
            SetNodeMap(rom, node);

            if (!DisplayComponents)
                node = null;

            LinkContains(node, Add(rom.Address, rom.Name));
            LinkContains(node, Add(rom.Data, rom.Name));
            LinkContains(node, Add(rom.ChipEnable, rom.Name));
            LinkContains(node, Add(rom.OutputEnable, rom.Name));

            return node;
        }

        public DirectedGraphNode Add<AddressT, DataT>(MemoryRam<AddressT, DataT> ram)
            where AddressT : BusData, new()
            where DataT : BusData, new()
        {
            if (!Visit(ram))
                return FindNode(ram);

            DirectedGraphNode node = AddNode<MemoryRam<AddressT, DataT>>(ram.Name);
            SetNodeMap(ram, node);

            if (!DisplayComponents)
                node = null;

            LinkContains(node, Add(ram.Address, ram.Name));
            LinkContains(node, Add(ram.Data, ram.Name));
            LinkContains(node, Add(ram.ChipEnable, ram.Name));
            LinkContains(node, Add(ram.OutputEnable, ram.Name));
            LinkContains(node, Add(ram.WriteEnable, ram.Name));

            return node;
        }

        public DirectedGraphNode Add(CpuZ80.CpuZ80 cpu)
        {
            if (!Visit(cpu))
                return FindNode(cpu);

            DirectedGraphNode node = AddNode<CpuZ80.CpuZ80>(cpu.Name);
                SetNodeMap(cpu, node);

            if (!DisplayComponents)
                node = null;

            LinkContains(node, Add(cpu.Address, cpu.Name));
            LinkContains(node, Add(cpu.Data, cpu.Name));
            LinkContains(node, Add(cpu.BusAcknowledge, cpu.Name));
            LinkContains(node, Add(cpu.BusRequest, cpu.Name));
            LinkContains(node, Add(cpu.Clock, cpu.Name));
            LinkContains(node, Add(cpu.Halt, cpu.Name));
            LinkContains(node, Add(cpu.Interrupt, cpu.Name));
            LinkContains(node, Add(cpu.IoRequest, cpu.Name));
            LinkContains(node, Add(cpu.MachineCycle1, cpu.Name));
            LinkContains(node, Add(cpu.MemoryRequest, cpu.Name));
            LinkContains(node, Add(cpu.NonMaskableInterrupt, cpu.Name));
            LinkContains(node, Add(cpu.Read, cpu.Name));
            LinkContains(node, Add(cpu.Refresh, cpu.Name));
            LinkContains(node, Add(cpu.Reset, cpu.Name));
            LinkContains(node, Add(cpu.Wait, cpu.Name));
            LinkContains(node, Add(cpu.Write, cpu.Name));

            return node;
        }

        private DirectedGraphNode AddNode<T>(string name = null, string ownerName = null, string netName = null)
        {
            var id = _idBuilder.NewId<T>(name, ownerName, netName);
            var node = _dgmlBuilder.AddNode(id);
            node.Label = id;
            node.TypeName = typeof(T).Name;
            node.Category1 = node.TypeName;

            return node;
        }

        private void SetNodeMap(object component, DirectedGraphNode node)
        {
            if (!_visited.ContainsKey(component))
                throw new ArgumentException("Component was not visited.", nameof(component));

            _visited[component] = node;
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

        private DirectedGraphNode FindNode<T>(T component)
        {
            DirectedGraphNode node = null;
            _visited.TryGetValue(component, out node);
            return node;
        }

        private bool Visit(object obj)
        {
            if (_visited.ContainsKey(obj))
                return false;

            _visited.Add(obj, null);
            return true;
        }

        private string SafeNetName(DigitalSignalProvider provider)
        {
            if (provider == null || !provider.IsConnected) return null;
            return provider.DigitalSignal.Name;
        }

        private string SafeNetName(DigitalSignalConsumer consumer)
        {
            if (consumer == null || !consumer.IsConnected) return null;
            return consumer.DigitalSignal.Name;
        }

        private string SafeNetName(BusMaster master)
        {
            if (master == null || !master.IsConnected) return null;
            return master.Bus.Name;
        }

        private string SafeNetName(BusSlave slave)
        {
            if (slave == null || !slave.IsConnected) return null;
            return slave.Bus.Name;
        }
    }
}
